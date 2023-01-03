using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogController : MonoBehaviour {
    Animator anim;
    int randChoice;

    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
        InvokeRepeating("ChooseAnim", 10f, 5f);
    }
	
	// Update is called once per frame
	void Update () {

        
        
    }

    void ChooseAnim()
    {
        randChoice = Random.Range(1, 16);

        if (randChoice > 4 && randChoice < 12)
        {
            //Stays
        }
        else if (randChoice < 4)
        {
            anim.SetTrigger("surprised");
        }
        else if (randChoice > 12)
        {
            anim.SetTrigger("happy");
        }
    }

}
