using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;


public class FinishLevel : MonoBehaviour {
    public bool lastLevel = false;
    public float loadTimer = 0;
    [Header("This is the index for the scene you wish to load, see File->Build Settings")]
    public int sceneIndex = 0;
    [Header("Amount of time(in seconds) player must stand in trigger to reload the level.")]
    public float timeBeforeLoad = 1;

    private void OnTriggerStay(Collider other)
    {
        //Add deltaTime to the load timer
        loadTimer += Time.deltaTime;
        //When the timer reaches the set delay time, load the specefied scene.
        if (loadTimer >= timeBeforeLoad)
        {
            if (other.tag == "Player" && !lastLevel)
            {
                SceneManager.LoadScene(sceneIndex);
            }
            else if (other.tag == "Player" && lastLevel)
            {
                //Game's over, print out neccessary win screen
            }

        }
    }

}
