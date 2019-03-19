//---------------------------------------------------------
//-- Peter Matesic
//-- Used for revealing / hiding the pause menu
//---------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    GameObject[] pauseObjects;
    //public GameObject postProcessGameObject; // To add a blurr effect
    public bool isPaused;

    // Use this for initialization
    void Start()
    {
        Time.timeScale = 1;
        pauseObjects = GameObject.FindGameObjectsWithTag("ShowOnPause");
        hidePaused();
        isPaused = false;
    }

    // Update is called once per frame
    void Update()
    {
        //uses the Escape button to pause and unpause the game
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                hidePaused();
            }
            else
            {
                showPaused();
            }
        }
    }

    //shows objects with ShowOnPause tag
    public void showPaused()
    {
        foreach (GameObject g in pauseObjects)
        {
            g.SetActive(true);
        }
        isPaused = true;
        Time.timeScale = 0;
        Cursor.visible = true;
        //postProcessGameObject.SetActive(true);
    }

    //hides objects with ShowOnPause tag
    public void hidePaused()
    {
        foreach (GameObject g in pauseObjects)
        {
            g.SetActive(false);
        }
        isPaused = false;
        Time.timeScale = 1;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
    }
}

