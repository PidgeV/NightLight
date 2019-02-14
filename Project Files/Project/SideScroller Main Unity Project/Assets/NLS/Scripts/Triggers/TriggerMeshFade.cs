using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerMeshFade : MonoBehaviour {
    public GameObject objectToFade;    
    public float transistionSpeed = 1;
    public bool fadeChildrenObjects = false;

    private float blendValue = 0;
    private bool yennoIn = false;
    private bool fenIn = false;
    private Color initialColor;
    private Color invisColor;

    // Use this for initialization
    void Start()
    {
        initialColor = objectToFade.GetComponent<Renderer>().material.color;
        invisColor = objectToFade.GetComponent<Renderer>().material.color;
        invisColor.a = 0;
    }

    // Update is called once per frame
    void Update()
    {

        //Increase/decrease alpha if player is inside/outside collider
        if (yennoIn || fenIn)
        {
            if (blendValue < 1) blendValue += transistionSpeed * Time.deltaTime;
            else blendValue = 1;
            
            if (fadeChildrenObjects)
            {
                foreach(Transform child in transform)
                {
                    objectToFade.GetComponent<Renderer>().material.color = Color32.Lerp(initialColor, invisColor, blendValue);
                }
            }
            else
            {
                objectToFade.GetComponent<Renderer>().material.color = Color32.Lerp(initialColor, invisColor, blendValue);
            }
        }
        else
        {            
            if (blendValue > 0) blendValue -= transistionSpeed * Time.deltaTime;
            else blendValue = 0;

            if (fadeChildrenObjects)
            {
                foreach (Transform child in transform)
                {
                    objectToFade.GetComponent<Renderer>().material.color = Color32.Lerp(initialColor, invisColor, blendValue);
                }
            }
            else
            {
                objectToFade.GetComponent<Renderer>().material.color = Color32.Lerp(initialColor, invisColor, blendValue);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            yennoIn = true;
        }
        else if (other.tag == "Player2" && !GameObject.FindGameObjectWithTag("LevelControl").GetComponent<SwitchCharacterControl>().onPlayer1)
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
