using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//Set up and display category menu
public class CategoryMenu : MonoBehaviour
{
    //Int (0 or 1) stored to determine whether the user has completed each category
    int cat1Completed, cat2Completed, cat3Completed, cat4Completed, cat5Completed;

    //Lock pictures to show when category is locked
    public GameObject lock1, lock2, lock3, lock4;

    bool inMenu;
    private Text sliderText;

    void Start()
    {
        //Get values of which categories are complete
        GetCatsComplete();

        //Show Category Menu
        DebugUIBuilder.instance.AddLabel("CATEGORY MENU");

        //If the user has beaten the game, allow them to visit "Winner's Grove"
        if (cat5Completed == 1)
        {
            DebugUIBuilder.instance.AddButton("Winner's Grove", ChooseWin);
        }

        //Build buttons, declare what functions called on press
        DebugUIBuilder.instance.AddButton("FBLA History", ChooseHistory);
        DebugUIBuilder.instance.AddButton("FBLA Officers", ChooseOfficers);
        DebugUIBuilder.instance.AddButton("FBLA Events", ChooseEvents);
        DebugUIBuilder.instance.AddButton("Parli. Procedure", ChooseParliamentary);
        DebugUIBuilder.instance.AddButton("FBLA Attire", ChooseAttire);
        DebugUIBuilder.instance.AddButton("Back", BackToMenu);

        //Show built objects
        DebugUIBuilder.instance.Show();
        inMenu = true;
    }



    void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.Two) || OVRInput.GetDown(OVRInput.Button.Start))
        {
            if (inMenu) DebugUIBuilder.instance.Hide();
            else DebugUIBuilder.instance.Show();
            inMenu = !inMenu;
        }
    }

    void GetCatsComplete()
    {
        //Get catXCompleted value, 0 or 1, from PlayerPrefs (unity storage for preferences),
        //Initialize catXCompleted values from PlayerPrefs
        cat1Completed = PlayerPrefs.GetInt("cat1Completed");
        cat2Completed = PlayerPrefs.GetInt("cat2Completed");
        cat3Completed = PlayerPrefs.GetInt("cat3Completed");
        cat4Completed = PlayerPrefs.GetInt("cat4Completed");
        cat5Completed = PlayerPrefs.GetInt("cat5Completed");


        //If user has completed a category, disable the lock image,
        //Otherwise show the lock
        if (cat1Completed == 1)
            lock1.SetActive(false);
        else
            lock1.SetActive(true);

        if (cat2Completed == 1)
            lock2.SetActive(false);
        else
            lock2.SetActive(true);

        if (cat3Completed == 1)
            lock3.SetActive(false);
        else
            lock3.SetActive(true);

        if (cat4Completed == 1)
            lock4.SetActive(false);
        else
            lock4.SetActive(true);
    }

    //Functions load each category on call
    void ChooseHistory()
    {
        SceneManager.LoadScene("History");
    }

    //If the previous category has been completed, allow user to load chosen category
    void ChooseOfficers()
    {
        if (cat1Completed == 1)
        {
            SceneManager.LoadScene("Officers");
        }
    }

    void ChooseEvents()
    {
        if (cat2Completed == 1)
        {
            SceneManager.LoadScene("Events");
        }
    }

    void ChooseParliamentary()
    {
        if (cat3Completed == 1)
        {
            SceneManager.LoadScene("ParliamentaryProcedure");
        }
    }

    void ChooseAttire()
    {
        if (cat4Completed == 1)
        {
            SceneManager.LoadScene("Attire");
        }
    }

    void ChooseWin()
    {
        SceneManager.LoadScene("WinScene");
    }

    void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}

