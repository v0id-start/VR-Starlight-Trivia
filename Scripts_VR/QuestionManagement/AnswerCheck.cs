using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Checks if user has selected the correct answer
public class AnswerCheck : MonoBehaviour {

    //Effects for right/wrong on each question
    public GameObject correctEffect;
    public GameObject wrongEffect;
    public Vector3 effectPos;

    //Access QuestionsManager script for functions & variables
    public QuestionsManager questionsManager;

    //Access floatScript
    //(Needs to be disabled to be able to change position of answer cube)
    private ObjectFloat floatScript;

    BoxCollider col;
    // Use this for initialization
    void Start () {
        //store float script
        floatScript = GetComponent<ObjectFloat>();

    }
	
    //When answer cube detects the ball, if answer cube is tagged correct, Start Correct Coroutine,
    //Otherwise Start Wrong Coroutine
    //Finally disable ball that hit the cube
    void OnCollisionEnter(Collision other)
    {
        if (gameObject.tag == "Correct")
        {   
            other.gameObject.SetActive(false);
            StartCoroutine(Correct());
        }
        else if (gameObject.tag == "Wrong")
        {
            other.gameObject.SetActive(false);
            StartCoroutine(Wrong());
        }
    }
    

    //When user answers correctly
    IEnumerator Correct()
    {
        DisableCubes();

        //Teleport cube offscreen, instantiate correct effect in it's place,
        //call Correct function from QuestionsManager to add score
        Debug.Log("Correct!");
        floatScript.enabled = false;
        gameObject.transform.position = new Vector3(-1.7f, -2f, 1.6f);
        Instantiate(correctEffect, effectPos, Quaternion.identity);
        questionsManager.CorrectAnswer();

        //Wait 2 seconds
        yield return new WaitForSeconds(2);

        //Call ResetVars from question manager to set up for next question,
        //allow cube to float again, and pick the next random question
        questionsManager.ResetVars();
        floatScript.enabled = true;
        questionsManager.PickRandomQuestion(questionsManager.categoryQuestions);
    }

    //When user answers incorrectly
    IEnumerator Wrong()
    {
        DisableCubes();

        //Teleport cube offscreen, instantiate correct effect in it's place,
        //call Correct function from QuestionsManager to add score
        Debug.Log("Wrong!");
        floatScript.enabled = false;
        gameObject.transform.position = new Vector3(-1.7f, -2f, 1.6f);
        Instantiate(wrongEffect, effectPos, Quaternion.identity);
        questionsManager.WrongAnswer();

        //Wait 2 seconds
        yield return new WaitForSeconds(2);

        //Call ResetVars from question manager to set up for next question,
        //allow cube to float again, and pick the next random question
        questionsManager.ResetVars();
        floatScript.enabled = true;
        questionsManager.PickRandomQuestion(questionsManager.categoryQuestions);
    }

    void DisableCubes()
    {
        questionsManager.answer1Cube.GetComponent<BoxCollider>().enabled = false;
        questionsManager.answer2Cube.GetComponent<BoxCollider>().enabled = false;
        questionsManager.answer3Cube.GetComponent<BoxCollider>().enabled = false;
    }
}
