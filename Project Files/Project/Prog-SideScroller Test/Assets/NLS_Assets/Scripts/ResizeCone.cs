using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResizeCone : MonoBehaviour
{
    public Light currentLight;
    GameObject parentLight;
    BlakeTestRays_DblDmg rays;
    public float temp;
   
    // Use this for initialization
    void Start()
    {
        //currentLight = GetComponent<Light>();
        parentLight = transform.parent.gameObject;
        rays = parentLight.GetComponent<BlakeTestRays_DblDmg>();
        this.transform.gameObject.layer = 2;
    }

    // Update is called once per frame
    void Update()
    {
        temp = Mathf.Tan(currentLight.spotAngle * Mathf.Deg2Rad/2) * currentLight.range;
        transform.localScale = new Vector3(temp, temp, currentLight.range);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            rays.playerIn = true;
        }
        else if (other.tag == "Player2")
        {
            rays.player2In = true;
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            rays.playerIn = false;
            rays.rayHitP1 = false;
            //other.GetComponent<PlayerLightCounter>().isInLight = false;
        }
        else if (other.tag == "Player2")
        {
            rays.player2In = false;
            rays.rayHitP2 = false;
            //other.GetComponent<PlayerLightCounter>().isInLight = false;
        }

    }
}
