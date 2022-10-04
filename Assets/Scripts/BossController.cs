using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
public class BossController : MonoBehaviour
{
    /*
     The boss fight:
        - Should first walk into the scene then trigger dialogue (Entering state)
            - When dialogue ends, boss health bar appears at bottom of screen;
            - maybe player health bar too
        - Should follow player around (following State)
        - If the player is too far away, do a charge attack (charging State) (-3hp)
            - readying state -> looks towards direction while staying still
            - charging state -> goes until collision
            - finished state -> pauses for length of time
        - If the player is close, do a punch (punching (doesn't need to be a state)) (-1hp)
            - play punch animation and look at player
            - move speed slower
        - When the boss is defeated, the boss stays still (defeated state)
            - player has to arrest the boss
     */

    public enum BossState {entering, following, charging, defeated,afterDefeat}
    public enum ChargeStates {readying, preCharge, charging, finished }

    public NavMeshAgent agent;

    public float hp = 1f;
    public float maxHP = 20f;
    public Image bossHPUI;
    public GameObject showUI;
    public Image playerHPUI;

    public BossState bState = BossState.entering;

    public Transform originPoint;
    public Transform moveToPoint;
    public float TimeToGetToPoint = 1f;
    public float curTimeToPoint = 0f;

    public PlayerController target;
    public float followSpeed = 3f;
    public float chargeSpeed = 15;
    public float attackingSpeed = 1.5f;

    [Header("Animation")]
    public Animation anim;
    public int animToPlay = 0;
    public AnimationClip a1;
    public AnimationClip a2;
    public AnimationClip a3;
    public AnimationClip c1;
    public AnimationClip c2;
    public AnimationClip c3;
    public AnimationClip walk;
    public AnimationClip death;
    public bool isAttacking = false;

    public ChargeStates cState = ChargeStates.readying;
    public float[] cstateTimes;
    public float curTime;
    public Vector3 goToPoint;

    public GameObject arrestTrigger;

    public ParticleSystem getHit = null;
    // Start is called before the first frame update
    void Start()
    {
        hp = maxHP;
        agent.speed = followSpeed;
        showUI.SetActive(false);
        arrestTrigger.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        playerHPUI.fillAmount = (target.hp / target.maxHP);
        bossHPUI.fillAmount = (hp / maxHP);
        if (curTime> 0) { curTime -= Time.deltaTime; }
        
        switch (bState)
        {
            case BossState.entering:
                anim.clip = walk;
                anim.Play();
                agent.enabled = false;
                transform.position = Vector3.Lerp(originPoint.position, moveToPoint.position, curTimeToPoint);
                if(curTimeToPoint <= TimeToGetToPoint)
                {
                    curTimeToPoint += Time.deltaTime;
                } else
                {
                    //trigger dialogue TODO
                    //then
                    bState = BossState.following;
                    agent.enabled = true;
                    showUI.SetActive(true);
                }
                break;
            case BossState.following:
                if (!isAttacking)
                {
                    anim.clip = walk;
                    anim.Play();
                }
                transform.LookAt(target.transform);
                agent.SetDestination(target.transform.position);
                Vector3 MoveDir = (target.transform.position - this.transform.position);
                float distFromPlayer = MoveDir.magnitude;
                if(distFromPlayer <= 3f && !isAttacking)
                {
                    StartCoroutine(Attack());
                }
                if(distFromPlayer >= 7f && !isAttacking)
                {
                    bState = BossState.charging;
                    cState = ChargeStates.readying;
                    curTime = cstateTimes[0];
                }
                break;
            case BossState.charging:
                switch (cState)
                {
                    case ChargeStates.readying:
                        anim.clip = c1;
                        anim.Play();
                        agent.SetDestination(transform.position);
                        transform.LookAt(target.transform);
                        if (curTime <= 0) {
                            cState = ChargeStates.preCharge;
                            curTime = cstateTimes[1];
                        }
                        break;

                    case ChargeStates.preCharge:
                        RaycastHit hit;
                        if (Physics.Raycast(transform.position, transform.forward, out hit, 100))
                        {
                            goToPoint= hit.point;
                        }
                        if (curTime <= 0)
                        {
                            cState = ChargeStates.charging;
                            curTime = cstateTimes[2];
                            agent.speed = chargeSpeed;
                            agent.stoppingDistance = 0f;
                            agent.SetDestination(goToPoint);
                        }
                        break;
                    
                    case ChargeStates.charging:
                        anim.clip = c2;
                        anim.Play();
                        if (curTime<=0)
                        {
                            anim.clip = c3;
                            anim.Play();
                            cState = ChargeStates.finished;
                            curTime = cstateTimes[3];
                        }
                        break;
                    case ChargeStates.finished:
                        agent.SetDestination(transform.position);
                        if (curTime <= 0)
                        {
                            bState = BossState.following;
                            agent.stoppingDistance = 2.5f;
                            agent.speed = followSpeed;
                        }
                        break;
                    default:
                        break;
                }
                break;
            case BossState.defeated:
                agent.enabled = false;
                showUI.SetActive(false);
                break;
            case BossState.afterDefeat:
                agent.enabled = false;
                showUI.SetActive(false);
                transform.eulerAngles = new Vector3(90f, transform.eulerAngles.y, 0f);
                break;

            default:
                break;
        }
        if(bState != BossState.defeated && bState != BossState.afterDefeat)
        {
            transform.eulerAngles = new Vector3(0f, transform.eulerAngles.y, 0f);
        }
        
    }

    public IEnumerator Attack()
    {
        agent.speed = attackingSpeed;
        animToPlay = Random.Range(0, 3);

        switch (animToPlay)
        {
            case 0:
                anim.clip = a1;
                anim.Play();
                break;
            case 1:
                anim.clip = a2;
                anim.Play();
                break;
            case 2:
                anim.clip = a3;
                anim.Play();
                break;
            default:
                anim.clip = a1;
                anim.Play();
                break;
        }
        isAttacking = true;
        yield return new WaitForSeconds(a1.length);
        isAttacking = false;
        agent.speed = followSpeed;
    }

    public IEnumerator Die()
    {
        bState = BossState.defeated;
        anim.clip = death;
        anim.Play();
        yield return new WaitForSeconds(death.length);
        arrestTrigger.SetActive(true);
        bState = BossState.afterDefeat;
    }
    public void TakeDamage(float amt)
    {
        if (hp > 0)
        {
            hp -= amt;
            GameObject.FindObjectOfType<CameraFollow>().shakeDuration = .15f;
            if (getHit != null)
            {
                getHit.Play();
            }
        }

        if (hp <= 0)
        {
            hp = 0;
            StartCoroutine(Die());
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerAttack") && bState != BossState.defeated)
        {
            TakeDamage(1f);
        }
    }
}
