using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Quits game
public class QuitGame : MonoBehaviour {

    void Start()
    {
        //In each new scene loaded, don't destroy this script object
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update () {

        //If escape key pressed at any time, close game application
            if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }
}
