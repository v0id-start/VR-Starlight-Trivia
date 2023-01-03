using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Manage spawning of the answer ball
public class AnswerBall : MonoBehaviour {
    //prefab version of itself to be spawned
    public GameObject answerBall;

    //Effect and position of where future balls will be spawned
    public GameObject spawnEffect;
    public Vector3 spawnPos = new Vector3(0.705f, 0.804f, -0.4f);


    //If the ball hits the ground or an answer cube,
    //destroy the ball and spawn another, along with the effect at the correct position (on the podium)
    void OnCollisionEnter(Collision other)
    {

        if (other.gameObject.CompareTag("Correct") || other.gameObject.CompareTag("Wrong") || other.gameObject.CompareTag("Ground"))
        {
            Instantiate(spawnEffect, spawnPos, Quaternion.identity);
            Instantiate(answerBall, spawnPos, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}

