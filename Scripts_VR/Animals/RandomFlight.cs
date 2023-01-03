using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomFlight : MonoBehaviour {
    Vector3 newLoc;

    public float speed = 1f;
    public float proximity = 1f;
    public Transform sphereCenter;
    public float sphereRadius = 10f;
    Animator anim;
    int flapStyle = 0;
    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
        newLoc = sphereCenter.position + Random.insideUnitSphere * sphereRadius;

    }

    // Update is called once per frame
    void Update () {

        if (Vector3.Distance(transform.position, newLoc) <= proximity)
        {
            int flapStyle = Random.Range(0, 2);

            if (flapStyle == 0)
            {
                anim.SetBool("isFlapping1", true);
                anim.SetBool("isFlapping2", false);
            }
                
            else if (flapStyle == 1)
            {
                anim.SetBool("isFlapping2", true);
                anim.SetBool("isFlapping1", false);
            }

            transform.LookAt(newLoc);
            newLoc = sphereCenter.position + Random.insideUnitSphere * sphereRadius;
            newLoc.y = Mathf.Abs(newLoc.y);
        }
        
        transform.position = Vector3.Lerp(transform.position, newLoc, Time.deltaTime * speed);
        
    }
}
