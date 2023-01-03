using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TurtleController : MonoBehaviour
{
    Animator anim;

    public float idleSpeed = 0.2f;
    public float wanderRadius;
    public float wanderTimer;

    private Transform target;
    private NavMeshAgent agent;
    private float timer;

    bool isHappy;
    bool isRetreated;

    public float retreatTime = 3f;

    int happyChance;
    void Start()
    {
        InvokeRepeating("RandomGetHappy", 5f, 5f);
        anim = GetComponent<Animator>();
        isHappy = false;
        isRetreated = false;
    }
    // Use this for initialization
    void OnEnable()
    {
        agent = GetComponent<NavMeshAgent>();
        timer = wanderTimer;
    }

    // Update is called once per frame
    void Update()
    {

        if (agent.velocity.magnitude <= idleSpeed)
        {
            anim.SetBool("isIdle", true);
        }
        else
        {
            anim.SetBool("isIdle", false);
        }

        if (!isRetreated && !isHappy)
        {
            timer += Time.deltaTime;

            if (timer >= wanderTimer)
            {
                Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
                agent.SetDestination(newPos);
                timer = 0;
            }
        }

    }

    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;

        randDirection += origin;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);

        return navHit.position;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("AnswerBall"))
        {
            Retreat();
        }
    }

    void Retreat()
    {
        isRetreated = true;
        anim.SetTrigger("retreat");
        StartCoroutine("Wait");
        isRetreated = false;
    }

    void RandomGetHappy()
    {
        happyChance = Random.Range(1, 9);

        if (happyChance > 6)
        {
            isHappy = true;
            anim.SetTrigger("happy");
            isHappy = false;
        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(retreatTime);
    }
}