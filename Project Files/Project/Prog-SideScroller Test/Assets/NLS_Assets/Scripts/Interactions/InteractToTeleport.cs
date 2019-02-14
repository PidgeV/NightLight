using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InteractToTeleport : MonoBehaviour
{
    public GameObject levelControlObject;
    public GameObject teleportTo;
    public bool fenIn = false;
    public float transistionSpeed = 1;

    [Header("Not required:")]
    public GameObject fen;

    TextMeshPro text;
    private Color clr;

    private float blendValue = 0;    

    // Use this for initialization
    void Start()
    {
        text = GetComponent<TextMeshPro>();

        clr = text.faceColor;
        clr.a = 0;

        if (fen == null) fen = GameObject.FindGameObjectWithTag("Player2");
    }

    // Update is called once per frame
    void Update()
    {

        //Increase/decrease alpha if player is inside/outside collider
        if (fenIn)
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

        //Allows click to teleport
        if (Input.GetButtonDown("Activate") && !levelControlObject.GetComponent<SwitchCharacterControl>().onPlayer1 && fenIn && fen.GetComponent<TeleportDelay>().CanTeleport())
        {
            fen.transform.position = teleportTo.transform.position;
            fen.GetComponent<TeleportDelay>().ResetTime();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player2" && !levelControlObject.GetComponent<SwitchCharacterControl>().onPlayer1)
        {
            fenIn = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player2")
        {
            fenIn = false;
            fen.GetComponent<PlayerLightCounter>().isInLight = false;
        }
    }
}
