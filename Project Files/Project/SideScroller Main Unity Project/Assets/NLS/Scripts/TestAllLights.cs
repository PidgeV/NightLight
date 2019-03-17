//This checks every light in the scene if each character is in light or not and adjusts Trixies damage script
//if conditions are met

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAllLights : MonoBehaviour
{
    [Header("No more than one of these can be present in a level.")]
    [Header("Put in master control entity.")]
    GameObject[] lights;
    public GameObject yenno;
    public GameObject fen;
    int lightHitCountYenno;
    int lightHitCountFen;

    // Use this for initialization
    void Start()
    {
        lights = GameObject.FindGameObjectsWithTag("Light");

        if (yenno == null) yenno = GameObject.FindGameObjectWithTag("Player");
        if (fen == null) fen = GameObject.FindGameObjectWithTag("Player2");
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < lights.Length; i++)
        {
            if (lights[i].GetComponent<BlakeTestRays_DblDmg>() != null)
            {
                if (lights[i].GetComponent<BlakeTestRays_DblDmg>().rayHitP1)
                {
                    lightHitCountYenno++;
                }

                if (lights[i].GetComponent<BlakeTestRays_DblDmg>().rayHitP2)
                {
                    lightHitCountFen++;
                }
            }
        }

        if (lightHitCountYenno > 0)
            yenno.GetComponent<PlayerLightCounter>().isInLight = true;
        else yenno.GetComponent<PlayerLightCounter>().isInLight = false;

        if (lightHitCountFen > 0)
            fen.GetComponent<PlayerLightCounter>().isInLight = true;
        else
            fen.GetComponent<PlayerLightCounter>().isInLight = false;

        lightHitCountYenno = 0;
        lightHitCountFen = 0;
    }
}
