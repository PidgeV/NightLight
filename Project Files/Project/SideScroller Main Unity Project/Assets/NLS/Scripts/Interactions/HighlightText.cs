//---------------------------------------------------------
//-- Peter Matesic
//-- Used as a replacement for a button to implement a 
//-- glow effect as well as button funcionality
//---------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HighlightText : Selectable, IPointerEnterHandler, ISelectHandler  
{
    public GameObject levelControl;
    public GameObject defaultText;
    public GameObject highLightText;
    private BaseEventData m_BaseEvent;
    public enum ButtonEvent { PLAY, MAIN_MENU, QUIT};
    public ButtonEvent pauseEvent;

    void Update()
    {
        if (IsHighlighted(m_BaseEvent) == true)
        {
            defaultText.SetActive(false);
            highLightText.SetActive(true);
        }
        else if(IsPressed() == true)
        {
            if (pauseEvent == ButtonEvent.PLAY)
            {
                defaultText.SetActive(true);
                highLightText.SetActive(false);
                levelControl.GetComponent<PauseMenu>().hidePaused();
            }
            else if(pauseEvent == ButtonEvent.MAIN_MENU)
            {
                try
                {
                    Time.timeScale = 1;
                    Cursor.visible = true;
                    SceneManager.LoadScene(0);  // loads in main menu
                }
                catch (System.Exception e)
                {
                    Debug.Log(e.Message);
                }
            }
            else if(pauseEvent == ButtonEvent.QUIT)
            {
                Application.Quit();
            }
        }
        else
        {
            defaultText.SetActive(true);
            highLightText.SetActive(false);
        }
    }    
}
