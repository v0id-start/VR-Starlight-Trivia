using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestSounds : MonoBehaviour {
    public bool isMenu = false;

    // Use this for initialization
    //Play Forest Ambient Sounds when in forest level.
    void Start()
    {
        if (!isMenu)
        {
            FindObjectOfType<SoundManager>().Play("Forest");
        }
        else if (isMenu)
        {
            FindObjectOfType<SoundManager>().Pause("Forest");
        }
    }

}
