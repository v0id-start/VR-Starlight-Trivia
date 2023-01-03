using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//Set up and display category menu
public class MainMenu : MonoBehaviour
{

    bool inMenu;
    private Text sliderText;

    void Start()
    {

        //Build buttons, define what functions they call when pressed
        DebugUIBuilder.instance.AddLabel("MAIN MENU");
        DebugUIBuilder.instance.AddButton("Play", LoadCategoryMenu);
        DebugUIBuilder.instance.AddButton("Instructions", LoadInstructions);
        DebugUIBuilder.instance.AddButton("More Options", LoadMore);
        DebugUIBuilder.instance.AddButton("Credits", LoadCredits);
        DebugUIBuilder.instance.AddButton("Exit", ExitGame);
        
        //Show built buttons
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

    //Functions load scene specified when corresponding button is pressed
    void LoadCategoryMenu()
    {
        SceneManager.LoadScene("CategoryMenu");
    }

    void LoadInstructions()
    {
        SceneManager.LoadScene("Instructions");
    }

    void LoadMore()
    {
        SceneManager.LoadScene("More");
    }

    void LoadCredits()
    {
        SceneManager.LoadScene("Credits");
    }

    //Close application on call
    void ExitGame()
    {
        Application.Quit();
    }
}

