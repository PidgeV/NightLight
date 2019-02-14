using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    //Script to be placed on the object to be used as an elevator.  
    //Must have a triggering body attached, in order to trigger script.  
    //Either through GO with included extra script, or by default trigger

    public GameObject WaypointStart;
    public GameObject WaypointEnd;

    public bool Invert = false;

    public float SpeedOfElevator = 1.0f;
    public float AcceptableError = 0.2f;
    public bool EnteredTrigger = false;

    private bool startAni = false;
    private bool atStart;   //If true goes towards end, if false, goes towards start
    private Rigidbody rigidRB;
    private bool inKey = false;

    // Use this for initialization
    void Start()
    {
        rigidRB = gameObject.GetComponent<Rigidbody>();
    }


    // Update is called once per frame
    void Update()
    {
        if (inKey && EnteredTrigger == true)  //Keycode can be changed
        {
            startAni = true;
            if (((gameObject.transform.position.y <= WaypointStart.transform.position.y + AcceptableError) ||
                (gameObject.transform.position.y >= WaypointStart.transform.position.y - AcceptableError)) && Invert == false)
            {
                atStart = true;
            }
            else if (((gameObject.transform.position.y <= WaypointEnd.transform.position.y + AcceptableError) ||
                (gameObject.transform.position.y >= WaypointEnd.transform.position.y - AcceptableError)) && Invert == true)
            {
                atStart = true;
            }
            else
            {
                atStart = false;
            }
        }
        if (Invert == false)
        {
            if (startAni == true && atStart == true)
            {
                Vector3 direction = WaypointEnd.transform.position - gameObject.transform.position;
                rigidRB.MovePosition(gameObject.transform.position + (direction.normalized * SpeedOfElevator * Time.deltaTime));

                if ((gameObject.transform.position.y <= WaypointStart.transform.position.y + AcceptableError) ||
                    (gameObject.transform.position.y >= WaypointStart.transform.position.y - AcceptableError))
                {
                    startAni = false;
                }
            }
            else if (startAni == true && atStart == false)
            {
                Vector3 direction = WaypointStart.transform.position - gameObject.transform.position;
                rigidRB.MovePosition(gameObject.transform.position + (direction.normalized * SpeedOfElevator * Time.deltaTime));

                if ((gameObject.transform.position.y <= WaypointStart.transform.position.y + AcceptableError) ||
                    (gameObject.transform.position.y >= WaypointStart.transform.position.y - AcceptableError))
                {
                    atStart = true;
                }
            }
            if (((gameObject.transform.position.y <= WaypointStart.transform.position.y + AcceptableError) ||
                (gameObject.transform.position.y >= WaypointStart.transform.position.y - AcceptableError))
                && (startAni == true && atStart == true))
            {
                startAni = false;
            }
        }
        else if (Invert == true)
        {
            if (startAni == true && atStart == true)
            {
                Vector3 direction = WaypointStart.transform.position - gameObject.transform.position;
                rigidRB.MovePosition(gameObject.transform.position + (direction.normalized * SpeedOfElevator * Time.deltaTime));

                if ((gameObject.transform.position.y <= WaypointEnd.transform.position.y + AcceptableError) ||
                    (gameObject.transform.position.y >= WaypointEnd.transform.position.y - AcceptableError))
                {
                    startAni = false;
                }
            }
            else if (startAni == true && atStart == false)
            {
                Vector3 direction = WaypointStart.transform.position - gameObject.transform.position;
                rigidRB.MovePosition(gameObject.transform.position + (direction.normalized * SpeedOfElevator * Time.deltaTime));

                if ((gameObject.transform.position.y <= WaypointEnd.transform.position.y + AcceptableError) ||
                    (gameObject.transform.position.y >= WaypointEnd.transform.position.y - AcceptableError))
                {
                    atStart = true;
                }
            }
            if (((gameObject.transform.position.y <= WaypointEnd.transform.position.y + AcceptableError) ||
                (gameObject.transform.position.y >= WaypointEnd.transform.position.y - AcceptableError))
                && (startAni == true && atStart == true))
            {
                startAni = false;
            }
        }
        if (Input.GetKeyDown(KeyCode.Space))  //Keycode can be changed
        {
            inKey = true;
        }
    }


        private void OnTriggerEnter(Collider other)
    {
        EnteredTrigger = true;
    }

    private void OnTriggerExit(Collider other)
    {
        EnteredTrigger = false;
    }
}
