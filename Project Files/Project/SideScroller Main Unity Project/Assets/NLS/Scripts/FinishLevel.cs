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
    public GameObject spinner;

    public float smooth = 5.0f;
    float tiltAngle = 0.0f;

    public GameObject otherEnd;
    public bool atEnd;

    private void OnTriggerStay(Collider other)
    {
        //Add deltaTime to the load timer
        loadTimer += Time.deltaTime;
        //When the timer reaches the set delay time, load the specefied scene.
        if (loadTimer >= timeBeforeLoad)
        {
            if (other.tag == "Player" || other.tag == "Player2")
                atEnd = true;
            lastLevel = sceneIndex == maxIndex ? true : false;

            if (atEnd && otherEnd.GetComponent<FinishLevel>().atEnd == true && !lastLevel)
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

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player" || other.tag == "Player2")
            atEnd = false;
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

    public void SpinInc()
    {
        if (spinner)
        {
            tiltAngle += 120;
            //spinner.GetComponent<Transform>().rotation.y += (Mathf.Deg2Rad * 120);
        }
    }
    public void SpinDec()
    {
        if (spinner)
        {
            tiltAngle -= 120;
            //spinner.GetComponent<Transform>().rotation.y += (Mathf.Deg2Rad * 120);
        }
    }

    void Update()
    {
        // Smoothly tilts a transform towards a target rotation.
        float tiltAroundY = tiltAngle;

        Quaternion target = Quaternion.Euler(0, tiltAroundY, 0);

        // Dampen towards the target rotation
        spinner.transform.rotation = Quaternion.Slerp(spinner.transform.rotation, target, Time.deltaTime * smooth);
    }


}
