using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TriggerFocusClick : MonoBehaviour {
    public GameObject lookAt;
    TextMeshPro text;
    private Color clr;

    private bool inCollider = false;

    private float blendValue = 0;
    public float transistionSpeed = 1;

    public int ToTrigger = 0;
    public bool Replay = false;
    private bool triggered = false;

    // Use this for initialization
    void Start()
    {
        text = GetComponent<TextMeshPro>();

        clr = text.faceColor;
        clr.a = 0;
    }

    // Update is called once per frame
    void Update()
    {

        //Increase/decrease alpha if player is inside/outside collider
        if (inCollider)
        {
            if (blendValue > 0)
                blendValue -= transistionSpeed * Time.deltaTime;
            else
                blendValue = 0;

            text.faceColor = Color32.Lerp(text.color, clr, blendValue);
        }
        else
        {
            if (blendValue < 1)
                blendValue += transistionSpeed * Time.deltaTime;
            else
                blendValue = 1;

            text.faceColor = Color.Lerp(text.color, clr, blendValue);
        }

        //Allows click to activate lights
        if (Input.GetButtonDown("Activate") && inCollider)
        {
            if (!triggered)
            {
                if (lookAt != null)
                {
                    GameObject.FindGameObjectWithTag("LevelControl").GetComponent<FocusCamera>().TriggerSwitch(lookAt);
                    if (!Replay)
                        triggered = true;
                }
                else
                {
                    GameObject.FindGameObjectWithTag("LevelControl").GetComponent<FocusCamera>().TriggerSwitch(ToTrigger);
                    if (!Replay)
                        triggered = true;
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        inCollider = true;
    }
    private void OnTriggerExit(Collider other)
    {
        //if (triggered && Replay)
        //    triggered = false;
        inCollider = false;
    }

}
