using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MoreMenu : MonoBehaviour
{

    bool inMenu;
    private Text sliderText;

    void Start()
    {
        //Build buttons, define what functions they call
        DebugUIBuilder.instance.AddLabel("MORE OPTIONS");
        DebugUIBuilder.instance.AddButton("Reset Progress", ResetProgress);
        DebugUIBuilder.instance.AddButton("Back", BackToMenu);


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

    //Functions load scene specified
    void ResetProgress()
    {
        PlayerPrefs.DeleteAll();
    }

    void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
