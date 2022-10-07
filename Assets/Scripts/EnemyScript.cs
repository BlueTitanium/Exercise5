using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyScript : MonoBehaviour
{
    /*
     * Enemy can patrol
     * If player gets in front of sights, it will start attacking
     * 
     */
    public NavMeshAgent agent;
    public LineRenderer line;
    public float dist = 20f;
    public int curIndex = 0;
    public Transform[] patrolSpots;
    public bool isPatrolling;
    public bool shouldPatrol = true;
    public GameObject target = null;
    public float LoseTrackTime = 4f;
    public float curTime = 0f;
    public GameObject proj;

    public float shootTime = 0f;
    public float shootCD = 1f;

    public bool alive = true;
    public float hp = 10f;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public IEnumerator Die(float time)
    {
        alive = false;
        line.enabled = false;
        yield return new WaitForSeconds(time);
        var a = GetComponent<DropKey>();
        if (a != null)
        {
            a.dropKey();
        }
        Destroy(transform.parent.gameObject);
    }
    // Update is called once per frame
    void Update()
    {

        if (alive == true)
        {
            if(hp == 0)
            {
                StartCoroutine(Die(1f));
            }

            RaycastHit hit;

            if (curTime <= 0f)
            {
                shouldPatrol = true;
                target = null;
            }
            else
            {
                shouldPatrol = false;
                curTime -= Time.deltaTime;
            }
            if (target != null)
            {
                agent.SetDestination(target.transform.position);
                agent.stoppingDistance = 5f;
                if (shootTime <= 0)
                {
                    Instantiate(proj, line.transform.position, transform.rotation);
                    shootTime = shootCD;
                }
                else
                {
                    shootTime -= Time.deltaTime;
                }
            }
            else
            {
                shootTime = shootCD;
            }
            if (Physics.Raycast(transform.position, transform.forward, out hit, dist))
            {
                Vector3 localHit = transform.InverseTransformPoint(hit.point);
                line.SetPosition(1, localHit);
                if (hit.transform.gameObject.CompareTag("Player"))
                {
                    curTime = LoseTrackTime;
                    shouldPatrol = false;
                    isPatrolling = false;
                    target = hit.transform.gameObject;
                    transform.LookAt(target.transform);
                }
                else if (hit.transform.gameObject.CompareTag("EnemyProjectile") && target != null)
                {
                    transform.LookAt(target.transform);
                }
            }
            else
            {
                line.SetPosition(1, new Vector3(0, 0, dist));
            }

            if (target == null && agent.velocity == Vector3.zero)
            {
                StartCoroutine(CheckIfPatrolling());
            }

            if ((agent.pathEndPosition.x == transform.position.x && agent.pathEndPosition.z == transform.position.z && isPatrolling) || (isPatrolling == false && shouldPatrol == true))
            {
                agent.stoppingDistance = 0f;
                PatrolNext();
            }
        } else
        {
            agent.isStopped = true ;
        }
    }

    public IEnumerator CheckIfPatrolling()
    {
        yield return new WaitForSeconds(.1f);
        if(target == null && agent.velocity == Vector3.zero)
        {
            isPatrolling = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerAttack"))
        {
            hp -= 1;
            GameObject.FindObjectOfType<CameraFollow>().shakeDuration = .15f;
            curTime = LoseTrackTime;
            shouldPatrol = false;
            isPatrolling = false;
            target = GameObject.FindGameObjectWithTag("Player");
            transform.LookAt(target.transform);
        }
    }
    void PatrolNext()
    {
        agent.SetDestination(patrolSpots[curIndex].position);
        curIndex++;
        if(curIndex == patrolSpots.Length)
        {
            curIndex = 0;
        }
        isPatrolling = true;
    }
}
