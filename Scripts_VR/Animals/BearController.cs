using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearController : MonoBehaviour {
    Animator anim;
    bool triggered;

    void Start()
    {
        triggered = false;
        anim = GetComponent<Animator>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!triggered)
        {
            triggered = true;

            if (collision.gameObject.CompareTag("AnswerBall"))
            {
                anim.SetTrigger("wake");
                StartCoroutine("Wait");

                triggered = false;
            }
        }

    }


    IEnumerator Wait()
    {
        yield return new WaitForSeconds(4);
    }
}
