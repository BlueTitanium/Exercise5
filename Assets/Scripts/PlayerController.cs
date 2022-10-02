using System.Collections;
using System.Collections.Generic;
// MoveToClickPoint.cs
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    NavMeshAgent agent;
    public float maxHP = 10f;
    public float hp = 10f;
    public bool isAlive = true;
    [Header("Animation")]
    public Animation anim;
    public bool anim1Or2 = false; //true = a1, false = a2;
    public AnimationClip a1;
    public AnimationClip a2;
    public AnimationClip walk;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        var lm = GameObject.FindObjectOfType<LevelManager>();
        if (lm != null)
        {
            transform.position = lm.checkPoint;
        }
    }

    public void Die()
    {
        isAlive = false;
        agent.isStopped = true;
        gameObject.tag = "Untagged";
        GameObject.FindObjectOfType<GameManager>().ShowDeathScreen();
        
    }
    public void TakeDamage(float amt)
    {
        if(hp > 0)
        {
            hp -= amt;
            GameObject.FindObjectOfType<CameraFollow>().shakeDuration = .3f;
        }
        
        if(hp <= 0)
        {
            hp = 0;
            Die();
        }
    }
    void Update()
    {
        if (isAlive)
        {
            if ((!anim.isPlaying) && agent.velocity != Vector3.zero)
            {
                anim.clip = walk;
                anim.Play();
            }
            if (Input.GetMouseButton(0))
            {
                RaycastHit hit;

                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
                {
                    agent.SetDestination(hit.point);

                }
            }
            if (Input.GetMouseButtonDown(1))
            {
                Attack();
            }
        }        
    }

    public void Attack()
    {
        RaycastHit cHit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out cHit, 100))
        {
            transform.LookAt(cHit.point);
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
        }
        if (anim1Or2)
        {
            anim.clip = a1;
            anim.Play();
            
        } else
        {
            anim.clip = a2;
            anim.Play();
        }
        anim1Or2 = !anim1Or2;
    }

}