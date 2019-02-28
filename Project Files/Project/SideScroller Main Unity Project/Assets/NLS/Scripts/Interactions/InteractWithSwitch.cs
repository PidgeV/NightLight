﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

#if UNITY_EDITOR
using UnityEditor;
#endif

[System.Serializable]
public class InteractWithSwitch : MonoBehaviour
{
    public GameObject levelControlObject;

    //Animation variables
    [Header("Optional:")]
    [Tooltip("This is an array of animators that will play when triggering switch")]
    public Animator[] animator;
    [Tooltip("The string to trigger in the animator")]
    public string[] triggerString;

    public TextMeshPro text;
    public float transistionSpeed = 1;
    private Color clr;

    [Tooltip("If true, all lights will use the first bool in the array.")]
    public bool lightsSynchronized;
    public bool[] lightOnInitially;
    public Light[] lights;
    public float lightDelay = 0.0f;

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

        for (int i = 0; i < lights.Length; i++)
        {
            if (lights[i] != null && lightsSynchronized) lights[i].enabled = lightOnInitially[0];
            else if (lights[i] != null) lights[i].enabled = lightOnInitially[i];
        }
    }

    // Update is called once per frame
    void Update()
    {
        triggerDelay += Time.deltaTime;
        //Increase/decrease alpha if player is inside/outside collider
        if (yennoIn || fenIn)
        {
            if (blendValue > 0) blendValue -= transistionSpeed * Time.deltaTime;
            else blendValue = 0;
        }
        else
        {
            if (blendValue < 1) blendValue += transistionSpeed * Time.deltaTime;
            else blendValue = 1;
        }

        if (text != null) text.faceColor = Color.Lerp(text.color, clr, blendValue);  //Change text color if present

        //Allows click to activate lights
        if (Input.GetButtonDown("Activate") && levelControlObject.GetComponent<SwitchCharacterControl>().onPlayer1 && yennoIn)
        {
            if (triggerDelay >= lightDelay * 2)
            {
                StartCoroutine(ChangeLights());
                triggerDelay = 0;
            }
        }
        else if (Input.GetButtonDown("Activate") && !levelControlObject.GetComponent<SwitchCharacterControl>().onPlayer1 && fenIn)
        {
            if (triggerDelay >= lightDelay * 2)
            {
                StartCoroutine(ChangeLights());
                triggerDelay = 0;
            }
        }
    }

    public IEnumerator ChangeLights()
    {
        yield return new WaitForSeconds(lightDelay);

        //Change lights
        if (lightsSynchronized)
        {
            lightOnInitially[0] = !lightOnInitially[0];
        }

        for (int i = 0; i < lights.Length; i++)
        {
            if (!lightsSynchronized)
            {
                lightOnInitially[i] = !lightOnInitially[i];
                if (lights[i] != null) lights[i].enabled = lightOnInitially[i];
            }
            else
            {
                if (lights[i] != null) lights[i].enabled = lightOnInitially[0];
            }
        }

        //Run animations
        for (int i = 0; i < animator.Length; i++)
        {
            if (animator[i] != null)
            {
                animator[i].SetTrigger(triggerString[i]);
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

//---------------------------------------------------------------------------
//Custom inspector if animator isn't an array

#if UNITY_EDITOR
[CustomEditor(typeof(InteractWithSwitch))]
public class InteractWithSwitch_Editor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector(); // for other non-HideInInspector fields

        InteractWithSwitch script = target as InteractWithSwitch;

        // draw checkbox for the bool
        //script.interactWith = EditorGUILayout.Toggle(script.interactWith);
        if(GUILayout.Button("Switch lights"))
        {
            script.StartCoroutine(script.ChangeLights());
        }

        //if (script.hasAnimation) // if bool is true, show other fields
        {
            //script.focusDuringAnim = EditorGUILayout.ObjectField("Focus during Anim: ", script.focusDuringAnim, typeof(GameObject), true) as GameObject;
            //script.animator = EditorGUILayout.ObjectField("Animator:", script.animator, typeof(Animator), true) as Animator;
            //script.triggerString = EditorGUILayout.TextArea(script.triggerString) as string;
        }
    }
}
#endif
