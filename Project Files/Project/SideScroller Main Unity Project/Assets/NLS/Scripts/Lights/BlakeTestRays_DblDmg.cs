using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlakeTestRays_DblDmg : MonoBehaviour
{

    Light currentLight;
    GameObject player;
    GameObject player2;

    // Health hp;
    public bool playerIn = false;
    public bool player2In = false;
    public bool rayHitP1 = false;
    public bool rayHitP2 = false;

    float rayLength;


    // Use this for initialization
    void Start()
    {
        currentLight = GetComponent<Light>();
        player = GameObject.FindGameObjectWithTag("Player");
        player2 = GameObject.FindGameObjectWithTag("Player2");

        rayLength = currentLight.range / 2;
    }

    // Update is called once per frame
    void Update()
    {

        if (currentLight.enabled == true)
        {
            GetComponent<AuraAPI.AuraLight>().enabled = true;
        }
        else
        {
            GetComponent<AuraAPI.AuraLight>().enabled = false;
        }

        if (currentLight.enabled && playerIn)
        {
            rayLength = currentLight.range / 3 * 2;

            RaycastHit hit;

            if (Physics.SphereCast(currentLight.transform.position, 0.2f, player.transform.position - transform.position, out hit, rayLength))
            {
                PlayerLightCounter counter = player.GetComponent<PlayerLightCounter>();
                if (hit.collider.tag == "Player")
                {
                    Debug.DrawRay(currentLight.transform.position, player.transform.position - transform.position, Color.red);
                    
                    counter.isInLight = true;
                    rayHitP1 = true;
                }
                else
                {
                    Debug.DrawRay(currentLight.transform.position, player.transform.position - transform.position, Color.blue);
                    rayHitP1 = false;
                }
            }
        }
        if (currentLight.enabled && player2In)
        {
            rayLength = currentLight.range / 3 * 2;

            RaycastHit hit;

            if (Physics.SphereCast(currentLight.transform.position, 0.1f, player2.transform.position - transform.position, out hit, rayLength))
            {
                PlayerLightCounter counter = player2.GetComponent<PlayerLightCounter>();
                if (hit.collider.tag == "Player2")
                {
                    Debug.DrawRay(currentLight.transform.position, player2.transform.position - transform.position, Color.red);
                    rayHitP2 = true;
                }
                else
                {
                    Debug.DrawRay(currentLight.transform.position, player2.transform.position - transform.position, Color.blue);
                    rayHitP2 = false;
                }
            }
        }
    }
}