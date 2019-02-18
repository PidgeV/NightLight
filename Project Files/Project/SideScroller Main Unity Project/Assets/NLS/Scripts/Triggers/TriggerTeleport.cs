using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

#if UNITY_EDITOR
using UnityEditor;
#endif


public class TriggerTeleport : MonoBehaviour
{
    //Location Variables
    public bool interactWith = true;
    public GameObject levelControlObject;
    public GameObject teleportTo;
    private GameObject fen;

    private bool fenIn = false;    

    //Text variables
    public TextMeshPro text;
    private Color clr;
    private float blendValue = 0;
    // [HideInInspector]        Get help with this
    [Header("Blake, ask john about this")]
    public float textFadeSpeed = 1;

    //Reveal variables - usable with custom inspector
    //public bool hasAnimation = false;

    //Animation variables
    [Header("Optional:")][Tooltip("Camera will follow this during the duration of animation")]
    public GameObject focusDuringAnim;
    public Animator[] animator;
    [Tooltip("The string to trigger in the animator")]
    public string[] triggerString;

    // Use this for initialization
    void Start()
    {
        if (interactWith)
        {
            if(text == null) text = GetComponent<TextMeshPro>();

            clr = text.faceColor;
            clr.a = 0;
        }

        if (fen == null) fen = GameObject.FindGameObjectWithTag("Player2");
    }

    // Update is called once per frame
    void Update()
    {
        if (interactWith)
        {
            //Increase/decrease alpha if player is inside/outside collider
            if (fenIn)
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

            //Allows click to teleport
            if (Input.GetButtonDown("Activate") && !levelControlObject.GetComponent<SwitchCharacterControl>().onPlayer1 && fenIn && fen.GetComponent<TeleportDelay>().CanTeleport())
            {
                Teleport();
            }
        }
    }

    private void Teleport()
    {
        fen.transform.position = teleportTo.transform.position;
        fen.GetComponent<PlayerLightCounter>().isInLight = false;
        fen.GetComponent<TeleportDelay>().ResetTime();

        //if (hasAnimation)
        {
            for(int i = 0; i < animator.Length - 1; i++)
            {
                if(animator[i] != null)
                {
                    fen.SetActive(false);
                    Debug.Log("Disabled Fen");
                    animator[i].SetTrigger(triggerString[i]);
                    GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraFollow>().target = focusDuringAnim.transform;
                }                
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player2" && !levelControlObject.GetComponent<SwitchCharacterControl>().onPlayer1)
        {
            fenIn = true;
        }
        if(!interactWith)
        {
            Teleport();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player2")
        {
            fenIn = false;
        }
    }
}

//---------------------------------------------------------------------------
//Custom inspector if animator isn't an array

//#if UNITY_EDITOR
//[CustomEditor(typeof(TriggerTeleport))]
//public class TriggerTeleport_Editor : Editor
//{
//    public override void OnInspectorGUI()
//    {
//        DrawDefaultInspector(); // for other non-HideInInspector fields

//        TriggerTeleport script = target as TriggerTeleport;

//        // draw checkbox for the bool
//        //script.interactWith = EditorGUILayout.Toggle(script.interactWith);

//        if (script.hasAnimation) // if bool is true, show other fields
//        {
//            script.focusDuringAnim = EditorGUILayout.ObjectField("Focus during Anim: ",script.focusDuringAnim, typeof(GameObject), true) as GameObject;
//            script.animator = EditorGUILayout.ObjectField("Animator:", script.animator, typeof(Animator), true) as Animator;
//            script.triggerString = EditorGUILayout.TextArea(script.triggerString) as string;
//        }
//    }
//}
//#endif