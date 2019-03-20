using System.Collections;
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

    [Tooltip("If true, all lights will use the first bool in the array.")]
    public float lightDelay = 0.0f;
    public bool[] lightOnInitially;
    public Light[] lights;

    //Animation variables
    [Header("Optional:")]

    public TextMeshPro text;
    [Tooltip("In seconds")]
    public float textFadeSpeed = 1;
    private Color clr;

    public bool hasAnimation = false;
    [HideInInspector]
    [Tooltip("This is an array of animators that will play when triggering switch")]
    public Animator[] animator;
    [HideInInspector]
    [Tooltip("The string to trigger in the animator")]
    public string[] triggerString;

    private bool yennoIn = false;
    private bool fenIn = false;

    private float blendValue = 0;
    private float triggerDelay = 0;

    public AudioSource audioSource;

    // Use this for initialization
    void Start()
    {
        if (text == null) text = GetComponent<TextMeshPro>();
        clr = text.faceColor;
        clr.a = 0;

        for (int i = 0; i < lights.Length; i++)
        {
            lights[i].enabled = lightOnInitially[i];
        }
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

        //Allows click to activate lights
        if (Input.GetButtonDown("Activate") && levelControlObject.GetComponent<SwitchCharacterControl>().onPlayer1 && yennoIn)
        {
            if (triggerDelay >= lightDelay * 2)
            {
                StartCoroutine(ChangeLights());
                triggerDelay = 0;
                audioSource.Play();
            }
        }
        else if (Input.GetButtonDown("Activate") && !levelControlObject.GetComponent<SwitchCharacterControl>().onPlayer1 && fenIn)
        {
            if (triggerDelay >= lightDelay * 2)
            {
                StartCoroutine(ChangeLights());
                triggerDelay = 0;
                if(audioSource != null) audioSource.Play();
            }
        }
    }

    public IEnumerator ChangeLights()
    {
        yield return new WaitForSeconds(lightDelay);

        //Change lights
        for (int i = 0; i < lights.Length; i++)
        {
            if (lights[i] != null)
            {
                lights[i].enabled = !lights[i].enabled;
                BlakeTestRays_DblDmg damage = lights[i].GetComponent<BlakeTestRays_DblDmg>();
                if (damage.playerIn) damage.rayHitP1 = !damage.rayHitP1;
                if (damage.player2In) damage.rayHitP2 = !damage.rayHitP2;
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
//Custom inspector
#if UNITY_EDITOR
[CustomEditor(typeof(InteractWithSwitch))]
public class InteractWithSwitch_Editor : Editor
{
    int arraySize = 1;
    int lightSize = 1;

    public override void OnInspectorGUI()
    {
        InteractWithSwitch script = target as InteractWithSwitch;

        if (GUILayout.Button("Switch lights"))
        {
            script.StartCoroutine(script.ChangeLights());
        }

        DrawDefaultInspector(); // for other non-HideInInspector fields

        lightSize = EditorGUILayout.IntField("Number of lights:", lightSize);

        //--------------------------------------------------------------------------------------
        //This will resize and show lights
        //#region Lights
        //if (lightSize < 1) lightSize = 1;

        //Light[] lightTemp = new Light[lightSize];
        //bool[] boolTemp = new bool[lightSize];
        //for (int i = 0; i < lightSize; i++)
        //{
        //    if (i < script.lights.Length) lightTemp[i] = script.lights[i];
        //    if (i < script.lightOnInitially.Length) boolTemp[i] = script.lightOnInitially[i];
        //}

        //script.lights = new Light[lightSize];
        //script.lightOnInitially = new bool[lightSize];

        //for (int i = 0; i < lightSize; i++)
        //{
        //    if (i < lightTemp.Length) script.lights[i] = lightTemp[i];
        //    if (i < boolTemp.Length) script.lightOnInitially[i] = boolTemp[i];
        //}

        //GUILayout.Label("Lights:");
        //if (script.lightsSynchronized) script.lightOnInitially[0] = EditorGUILayout.Toggle("Light State", script.lightOnInitially[0]);
        //for (int i = 0; i < lightSize; i++)
        //{
        //    script.lights[i] = EditorGUILayout.ObjectField(script.lights[i], typeof(Light)) as Light;
        //    if(!script.lightsSynchronized) script.lightOnInitially[i] = EditorGUILayout.Toggle("Light State", script.lightOnInitially[i]);
        //}
        //#endregion

        if (script.hasAnimation) // if bool is true, show other fields
        {
            GUILayout.Label("\n Animator");
            arraySize = EditorGUILayout.IntField("Number of animators:", arraySize);

            //--------------------------------------------------------------------------------------
            //This will resize and show animators
            #region Animator
            Animator[] animTemp = new Animator[arraySize];
            for (int i = 0; i < arraySize; i++)
            {
                if (i < script.animator.Length) animTemp[i] = script.animator[i];
            }

            script.animator = new Animator[arraySize];
            for (int i = 0; i < arraySize; i++)
            {
                if (i < animTemp.Length) script.animator[i] = animTemp[i];
            }

            GUILayout.Label("Animators:");
            for (int i = 0; i < script.animator.Length; i++)
            {
                script.animator[i] = EditorGUILayout.ObjectField(script.animator[i], typeof(Animator)) as Animator;
            }
            #endregion

            //--------------------------------------------------------------------------------------
            //This will resize and show trigger strings
            #region TriggerString
            GUILayout.Label("Trigger Strings:");

            string[] temp = new string[arraySize];
            for (int i = 0; i < arraySize; i++)
            {
                if (i < script.triggerString.Length) temp[i] = script.triggerString[i];
            }

            script.triggerString = new string[arraySize];
            for (int i = 0; i < arraySize; i++)
            {
                if (i < temp.Length) script.triggerString[i] = temp[i];
            }

            for (int i = 0; i < script.triggerString.Length; i++)
            {
                script.triggerString[i] = EditorGUILayout.TextField(script.triggerString[i]); ;
            }
            #endregion
        }
    }
}
#endif
