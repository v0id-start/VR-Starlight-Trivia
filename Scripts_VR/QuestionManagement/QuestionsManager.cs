using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//Manages random question & text output
public class QuestionsManager : MonoBehaviour {

    //Declare new public array of type 'Questions',
    //Will be used later to store bank of questions for each category.
    public Question[] categoryQuestions = new Question[10];

    //public string used for concatenation of category name in win screen
    public string categoryName;


    //Timer boolean used to determine when timer should be running
    bool timerRunning = true;

    //Public Start time float used to determine how long the user gets for each question
    public float startTime = 10f;

    //Time left to check if user has surpassed timer
    float timeLeft;

    //End effect and position for after user has completed category
    public GameObject endEffect1;
    public Transform endEffectPos1;

    //Canvas
    public Canvas gameCanvas;
    public Canvas endCanvas;

    public Text endText;

    //Text elements for game canvas to be displayed in runtime
    public Text timer;
    public Text scoreText;
    public Text questionTextObject;
    public Text answer1Object;
    public Text answer2Object;
    public Text answer3Object;

    //Floating answer cube objects
    public GameObject answer1Cube;
    public GameObject answer2Cube;
    public GameObject answer3Cube;

    //Positions of answer cubes
    public Transform answerPos1;
    public Transform answerPos2;
    public Transform answerPos3;

    //Initialize random numbers that will be used to decide which questions and answers will show
    int rndQuestionNum;
    int rndWrongNum1 = 0;
    int rndWrongNum2 = 0;

    //Initialize score and total answers given
    int score = 0;
    int total = 0;

    //Initialize booleans
    bool categoryFinished = false;
    bool newQuestion = false;

    public string findCatCompleted = "cat1Completed";

    //Class Question holds properties including:
    //correct answers, possible wrong answers, wether question has been answered and question
    [System.Serializable]
    public class Question
    {
        public string question;
        public string correctAnswer;
        public string wrongAnswer1;
        public string wrongAnswer2;

        public bool answered;

        //Constructor with input parameters
        public Question(string ques, string ans, string wrAns1, string wrAns2, bool ansd)
        {
            question = ques;
            correctAnswer = ans;
            wrongAnswer1 = wrAns1;
            wrongAnswer2 = wrAns2;
            answered = ansd;
        }

        //Default Constructor for new Questions
        public Question()
        {
            question = "What is FBLA?";
            correctAnswer = "An organization.";
            wrongAnswer1 = "A party.";
            wrongAnswer2 = "A video game.";

            answered = false;
        }

    }
    
    //Construct 10 new 'Questions' with default parameters constructor
    //Properties of each question assigned in Inspector
    public Question question0 = new Question();
    public Question question1 = new Question();
    public Question question2 = new Question();
    public Question question3 = new Question();
    public Question question4 = new Question();
    public Question question5 = new Question();
    public Question question6 = new Question();
    public Question question7 = new Question();
    public Question question8 = new Question();
    public Question question9 = new Question();

    // Use this for initialization
    void Start() {

        //Initialize defaults of variables
        timeLeft = startTime;

        scoreText.text = "Score: 0";
        endText.text = "";

        gameCanvas.enabled = true;
        endCanvas.enabled = false;

        //Bring Questions with properties from inspector into the Questions Array to be picked out of
        categoryQuestions[0] = question0;
        categoryQuestions[1] = question1;
        categoryQuestions[2] = question2;
        categoryQuestions[3] = question3;
        categoryQuestions[4] = question4;
        categoryQuestions[5] = question5;
        categoryQuestions[6] = question6;
        categoryQuestions[7] = question7;
        categoryQuestions[8] = question8;
        categoryQuestions[9] = question9;


        //Pick a random question from the questions array
        PickRandomQuestion(categoryQuestions);

	}
	
	// Update is called once per frame
	void Update () {

        //Show timer to 2 decimals on game canvas
        //If time is less than 10, turn yellow, less than 5, turn red
            if (timerRunning == true)
        {
            timeLeft -= Time.deltaTime;
            timer.text = timeLeft.ToString("F2");
            if (timeLeft < 11f && timeLeft > 6f)
            {
                timer.color = Color.yellow;
            }
            else if (timeLeft < 6f)
            {
                timer.color = Color.red;
            }
            //If user runs out of time, Stop the timer and call OutOfTime function
            if (timeLeft <= 0)
            {
                timerRunning = false;
                timer.text = "0.00";
                StartCoroutine(OutOfTime());
            }
        }

        

            //If user has completed 5 questions, allow them to return to menu
            //If user completed final category, load win scene
        if (categoryFinished)
        {
            if (OVRInput.GetDown(OVRInput.Button.One))
            {
                if (categoryName == "Attire" && score == 50)
                {
                    SceneManager.LoadScene("WinScene");
                }
                else
                {
                    SceneManager.LoadScene("CategoryMenu");
                }
            }
        }

    }


    //Depending on which number is chosen from the random int variable,
    //Set the corresponding cube with number to be tagged Correct,
    //Set 2 non-chosen cubes to be tagged Wrong
    void SetCubeTags(int corr)
    {
        if (corr == 1)
        {
            answer1Cube.gameObject.tag = "Correct";
            answer2Cube.gameObject.tag = "Wrong";
            answer3Cube.gameObject.tag = "Wrong";
        }
        else if (corr == 2)
        {
            answer1Cube.gameObject.tag = "Wrong";
            answer2Cube.gameObject.tag = "Correct";
            answer3Cube.gameObject.tag = "Wrong";
        }
        else if (corr == 3)
        {
            answer1Cube.gameObject.tag = "Wrong";
            answer2Cube.gameObject.tag = "Wrong";
            answer3Cube.gameObject.tag = "Correct";
        }

    }
    
    //Reset variables to defaults for each question
    public void ResetVars()
    {
        //Ensure all colliders are active
        answer1Cube.GetComponent<BoxCollider>().enabled = true;
        answer2Cube.GetComponent<BoxCollider>().enabled = true;
        answer3Cube.GetComponent<BoxCollider>().enabled = true;

        timer.color = Color.white;

        questionTextObject.color = Color.white;
        newQuestion = false;
        rndWrongNum1 = 0;
        rndWrongNum2 = 0;
        answer1Cube.transform.position = answerPos1.position;
        answer2Cube.transform.position = answerPos2.position;
        answer3Cube.transform.position = answerPos3.position;

        timeLeft = startTime;
        timerRunning = true;
    }
    
    //Get & Set up non-seen random question from the questions array
    public void PickRandomQuestion(Question[] questionsArray)
    {

        //Check if user has completed 5 questions
        if (total == 5)
        {
            CategoryComplete();
            return;
        }



        //Pick next random question number to select a question from the array
        rndQuestionNum = Random.Range(0, 10);

        //If the random question has already been answered, pick new random question number
        if (questionsArray[rndQuestionNum].answered == true)
        {
            while (newQuestion == false)
            {
                rndQuestionNum = Random.Range(0, 5);

                if (questionsArray[rndQuestionNum].answered == false)
                {
                    newQuestion = true;
                }

            }
        }

        //Set 'answered' boolean from question chosen to true so it won't be chosen again
        questionsArray[rndQuestionNum].answered = true;


        //Make sure that the 2 wrong answers will be in different positions
        while (rndWrongNum1 == rndWrongNum2)
        {
            rndWrongNum1 = Random.Range(1, 4);
            rndWrongNum2 = Random.Range(1, 4);
        }

        //Initialize variables from each property of the randomly chosen question to be used locally for THIS question, 
        string qText = questionsArray[rndQuestionNum].question;
        string qAnswer = questionsArray[rndQuestionNum].correctAnswer;
        string qWrongAnswer1 = questionsArray[rndQuestionNum].wrongAnswer1;
        string qWrongAnswer2 = questionsArray[rndQuestionNum].wrongAnswer2;
        

        //Position the correct and wrong answers depending on the randomly chosen numbers
        //(Each possible random case of placement for answers on canvas)
        //Also calls SetCubeTags passing the correct number to set which cube is correct
        if (rndWrongNum1 == 1 && rndWrongNum2 == 2)
        {
            answer1Object.text = qWrongAnswer1;
            answer2Object.text = qWrongAnswer2;
            answer3Object.text = qAnswer;
            SetCubeTags(3);
        }
        else if (rndWrongNum1 == 1 && rndWrongNum2 == 3)
        {
            answer1Object.text = qWrongAnswer1;
            answer2Object.text = qAnswer;
            answer3Object.text = qWrongAnswer2;
            SetCubeTags(2);
        }
        else if (rndWrongNum1 == 2 && rndWrongNum2 == 1)
        {
            answer1Object.text = qWrongAnswer2;
            answer2Object.text = qWrongAnswer1;
            answer3Object.text = qAnswer;
            SetCubeTags(3);
        }
        else if (rndWrongNum1 == 2 && rndWrongNum2 == 3)
        {
            answer1Object.text = qAnswer;
            answer2Object.text = qWrongAnswer1;
            answer3Object.text = qWrongAnswer2;
            SetCubeTags(1);
        }
        else if (rndWrongNum1 == 3 && rndWrongNum2 == 1)
        {
            answer1Object.text = qWrongAnswer2;
            answer2Object.text = qAnswer;
            answer3Object.text = qWrongAnswer1;
            SetCubeTags(2);
        }
        else if (rndWrongNum1 == 3 && rndWrongNum2 == 2)
        {
            answer1Object.text = qAnswer;
            answer2Object.text = qWrongAnswer2;
            answer3Object.text = qWrongAnswer1;
            SetCubeTags(1);
        }

        //Display this randomly chosen question's question value
        questionTextObject.text = qText;



    }

    //When user gets question correct, wrong, or runs out of time:
    //play sound, increase/decrease score, set text color
    public void CorrectAnswer()
    {
        FindObjectOfType<SoundManager>().Play("Correct");
        timerRunning = false;
        score += 10;
        total++;
        scoreText.text = "Score: " + score.ToString();
        questionTextObject.color = Color.green;
        questionTextObject.text = "CORRECT!!!";
        
    }

    public void WrongAnswer()
    {
        FindObjectOfType<SoundManager>().Play("Wrong");
        timerRunning = false;
        total++;
        scoreText.text = "Score: " + score.ToString();
        questionTextObject.color = Color.red;
        questionTextObject.text = "Wrong";
    }

    //When user runs out of time also reset variables & pcick next random question from within script
    IEnumerator OutOfTime()
    {
        answer1Cube.GetComponent<BoxCollider>().enabled = false;
        answer2Cube.GetComponent<BoxCollider>().enabled = false;
        answer3Cube.GetComponent<BoxCollider>().enabled = false;

        FindObjectOfType<SoundManager>().Play("Wrong");
        total++;
        questionTextObject.text = "Out of Time!";
        questionTextObject.color = Color.red;
        yield return new WaitForSeconds(2);
        Debug.Log("RESETTING");
        
        ResetVars();
        PickRandomQuestion(categoryQuestions);
    }

    //When user answers 5 questions, hide answer cubes
    void CategoryComplete()
    {
        answer1Cube.SetActive(false);
        answer2Cube.SetActive(false);
        answer3Cube.SetActive(false);

        //Play sound effect
        FindObjectOfType<SoundManager>().Play("Win");

        //Stop timer and set category finished to true which will call the Category Complete function from Update
        timerRunning = false;
        categoryFinished = true;


        //Spawn rotation for instantiated effect to use
        Quaternion spawnRotation = Quaternion.Euler(-90, 0, 0);

        
        Debug.Log("CATEGORY COMPLETE!");

        //Hide game canvas, show end canvas
        gameCanvas.enabled = false;
        endCanvas.enabled = true;

        //Show different text on end canvas based on user's score, (concatenate public category name)
        //If user got all 5 random questions correct, instantiate fireworks effects
        if (score == 50)
        {
            PlayerPrefs.SetInt(findCatCompleted, 1);

            Instantiate(endEffect1, endEffectPos1.position, spawnRotation);
            endText.text = "CONGRAGULATIONS!!!\nYou got " + score + "/" + "50" + " Points on the " + categoryName + " Category!!!";
        }
        else if (score > 30 && score < 50)
        {
            endText.text = "So Close!\nYou got " + score + "/" + "50" + " Points on the " + categoryName + " Category!!!";
        }
        else if (score < 40)
        {
            endText.text = "Try Again!\nYou got " + score + "/" + "50" + " Points on the " + categoryName + " Category!!!";
        }


        
    }


}
