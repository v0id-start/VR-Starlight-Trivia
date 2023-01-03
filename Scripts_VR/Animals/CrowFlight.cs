using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowFlight : MonoBehaviour {
    Animator anim;

    public Transform player;

    public Transform[] flyToPoints;

    public float reachDistance;
    public float flySpeed;

    public int currentPoint;
    public float idleTime;

    bool isTraveling = true;
    Vector3 dir;
    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isTraveling)
        {
            //Get direction
            dir = flyToPoints[currentPoint].position - transform.position;

            //Fly to next point
            transform.position += dir * Time.deltaTime * flySpeed;
            transform.LookAt(flyToPoints[currentPoint]);

            if (dir.magnitude <= reachDistance)
            {
                if (currentPoint < flyToPoints.Length)
                {
                    AirLoop();
                    //Prepare next point in array to fly to
                    currentPoint++;
                }
            }
        }


        if (currentPoint == flyToPoints.Length)
        {
            isTraveling = false;
            StartCoroutine("Land");

        }
    }

    IEnumerator Land()
    {
        currentPoint = 0;
        transform.LookAt(player);
        anim.SetTrigger("Land");


        yield return new WaitForSeconds(idleTime);


        isTraveling = true;

    }

    void AirLoop()
    {
        if (currentPoint < flyToPoints.Length - 1)
        {
            int loopChance = Random.Range(1, 5);

            if (loopChance == 1)
                anim.SetTrigger("Loop");
        }

    }
}
