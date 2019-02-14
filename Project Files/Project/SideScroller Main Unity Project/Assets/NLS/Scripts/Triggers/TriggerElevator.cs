using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerElevator : MonoBehaviour {

    public GameObject GOElevator;
    //Placed on the trigger for an elevator

    private void OnTriggerEnter(Collider other)
    {
        if (GOElevator.GetComponent<Elevator>())
        {
            GOElevator.GetComponent<Elevator>().EnteredTrigger = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (GOElevator.GetComponent<Elevator>())
        {
            GOElevator.GetComponent<Elevator>().EnteredTrigger = false;
        }
    }
}
