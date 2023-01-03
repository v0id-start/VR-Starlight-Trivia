using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Allow user to return back to menu after winning
public class WinSceneController : MonoBehaviour {

    // Update is called once per frame
    void Update()
    {
        //Load menu scene when 'A' is pressed
        if (OVRInput.GetDown(OVRInput.Button.One))
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}
