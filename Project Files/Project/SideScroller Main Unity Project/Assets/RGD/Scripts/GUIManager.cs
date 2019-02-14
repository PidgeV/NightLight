using UnityEngine;
using System.Collections;

//ATTACH TO MAIN CAMERA, shows your health and coins
public class GUIManager : MonoBehaviour 
{	
	public GUISkin guiSkin;					//assign the skin for GUI display
	[HideInInspector]
	public int coinsCollected;

	private int coinsInLevel;
    private Health health;
    private Health health2;
    private SwitchCharacterControl levelControl;


    //setup, get how many coins are in this level
    void Start()
	{
        levelControl = GameObject.FindGameObjectWithTag("LevelControl").GetComponent<SwitchCharacterControl>();
        coinsInLevel = GameObject.FindGameObjectsWithTag("Coin").Length;
        //health = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
        health2 = GameObject.FindGameObjectWithTag("Player2").GetComponent<Health>();
    }

    //show current health and how many coins you've collected
    void OnGUI()
    {
        GUI.skin = guiSkin;
        GUILayout.Space(5f);

        if (levelControl.onPlayer1)
        {
            if (health)
                GUILayout.Label("Health: " + health.currentHealth);
            if (coinsInLevel > 0)
                GUILayout.Label("Cubes: " + coinsCollected + " / " + coinsInLevel);
        }
        else
        {
            if (health2)
                GUILayout.Label("Health: " + health2.currentHealth);
            if (coinsInLevel > 0)
                GUILayout.Label("Cubes: " + coinsCollected + " / " + coinsInLevel);
        }
    }
}