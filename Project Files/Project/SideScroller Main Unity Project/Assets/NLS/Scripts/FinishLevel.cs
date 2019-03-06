using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;


public class FinishLevel : MonoBehaviour {
    public bool lastLevel = false;
    public float loadTimer = 0;
    [Header("This is the index for the scene you wish to load, see File->Build Settings")]
    [Header("If wishing for 1 scene, set all 3 to the same value")]
    public int sceneIndex = 0;
    public int maxIndex = 0;
    public int minIndex = 0;
    [Header("Amount of time(in seconds) player must stand in trigger to reload the level.")]
    public float timeBeforeLoad = 1;

    private void OnTriggerStay(Collider other)
    {
        //Add deltaTime to the load timer
        loadTimer += Time.deltaTime;
        //When the timer reaches the set delay time, load the specefied scene.
        if (loadTimer >= timeBeforeLoad)
        {
            lastLevel = sceneIndex == maxIndex ? true : false;

            if (other.tag == "Player" && !lastLevel)
            {
                try
                {
                    SceneManager.LoadScene(sceneIndex);
                }
                catch (System.Exception e)
                {
                    Debug.Log(e.Message);
                }
            }
            else if (other.tag == "Player" && lastLevel)
            {
                //Game's over, print out neccessary win screen
            }

        }
    }

    public void GoToLevel()
    {
        try
        {
            SceneManager.LoadScene(sceneIndex);
        }
        catch (System.Exception e)
        {
            Debug.Log(e.Message);
        }
    }
    public void IncIndex()
    {
        sceneIndex++;
        if (sceneIndex > maxIndex)
            sceneIndex = minIndex;
    }
    public void DecIndex()
    {
        sceneIndex--;
        if (sceneIndex < minIndex)
            sceneIndex = maxIndex;
    }

}
