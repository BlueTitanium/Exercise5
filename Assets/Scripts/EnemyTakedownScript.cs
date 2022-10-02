using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTakedownScript : MonoBehaviour
{
    public GameObject text;
    public EnemyScript enemy;
    public bool canKill = false;
    // Start is called before the first frame update
    void Start()
    {
        text.SetActive(false);
        canKill = false;
    }

    // Update is called once per frame
    void Update()
    {
        transform.eulerAngles = Vector3.zero;
        if (canKill && (Input.GetMouseButtonDown(1)))
        {
            kill();
        }
    }
    void kill()
    {
        StartCoroutine(enemy.Die(1f));
        text.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && enemy.alive)
        {
            text.SetActive(true);
            canKill = true;
            //kill();
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && enemy.alive)
        {
            text.SetActive(true);
            canKill = true;
            //kill();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            text.SetActive(false);
            canKill = false;
        }
    }
}
