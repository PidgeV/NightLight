using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public class DisplayText : MonoBehaviour {

    public GameObject levelControlObject;

    public TextMeshPro text;
    [Tooltip("In seconds")]
    public float textFadeSpeed = 1;
    private Color clr;

    private bool yennoIn = false;
    private bool fenIn = false;

    private float blendValue = 0;
    private float triggerDelay = 0;

    // Use this for initialization
    void Start()
    {
        if (text == null) text = GetComponent<TextMeshPro>();
        clr = text.faceColor;
        clr.a = 0;
    }

    // Update is called once per frame
    void Update()
    {
        triggerDelay += Time.deltaTime;
        //Increase/decrease alpha if player is inside/outside collider
        if (yennoIn || fenIn)
        {
            if (blendValue > 0) blendValue -= textFadeSpeed * Time.deltaTime;
            else blendValue = 0;
        }
        else
        {
            if (blendValue < 1) blendValue += textFadeSpeed * Time.deltaTime;
            else blendValue = 1;
        }

        if (text != null) text.faceColor = Color.Lerp(text.color, clr, blendValue);  //Change text color if present  
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
