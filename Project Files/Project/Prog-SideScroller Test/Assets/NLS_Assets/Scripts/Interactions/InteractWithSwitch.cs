using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InteractWithSwitch : MonoBehaviour
{
    public GameObject levelControlObject;
    TextMeshPro text;
    private Color clr;

    public bool lightsOnInitially;
    public Light[] lights;

    public bool yennoIn = false;
    public bool fenIn = false;

    public float blendValue = 0;
    public float transistionSpeed = 1;

    // Use this for initialization
    void Start()
    {
        text = GetComponent<TextMeshPro>();

        clr = text.faceColor;
        clr.a = 0;

        for (int i = 0; i < lights.Length; i++)
        {
            if (lights[i] != null) lights[i].enabled = lightsOnInitially;
        }
    }

    // Update is called once per frame
    void Update()
    {

        //Increase/decrease alpha if player is inside/outside collider
        if (yennoIn || fenIn)
        {
            if (blendValue > 0) blendValue -= transistionSpeed * Time.deltaTime;
            else blendValue = 0;

            text.faceColor = Color32.Lerp(text.color, clr, blendValue);
        }
        else
        {
            if (blendValue < 1) blendValue += transistionSpeed * Time.deltaTime;
            else blendValue = 1;

            text.faceColor = Color.Lerp(text.color, clr, blendValue);
        }

        //Allows click to activate lights
        if (Input.GetButtonDown("Activate") && levelControlObject.GetComponent<SwitchCharacterControl>().onPlayer1 && yennoIn)
        {
            lightsOnInitially = !lightsOnInitially;
            for (int i = 0; i < lights.Length; i++)
            {
                if (lights[i] != null) lights[i].enabled = lightsOnInitially;
            }
        }
        else if (Input.GetButtonDown("Activate") && !levelControlObject.GetComponent<SwitchCharacterControl>().onPlayer1 && fenIn)
        {
            lightsOnInitially = !lightsOnInitially;
            for (int i = 0; i < lights.Length; i++)
            {
                if (lights[i] != null) lights[i].enabled = lightsOnInitially;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            yennoIn = true;
        }
        else if (other.tag == "Player2" && !levelControlObject.GetComponent<SwitchCharacterControl>().onPlayer1)
        {
            fenIn = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            yennoIn = false;
        }
        if (other.tag == "Player2")
        {
            fenIn = false;
        }
    }
}
