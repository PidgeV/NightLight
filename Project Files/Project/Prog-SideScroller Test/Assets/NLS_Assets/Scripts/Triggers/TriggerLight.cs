using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerLight : MonoBehaviour {

    public GameObject GOLight;
    private bool triggering;
    //Placed on the trigger for a light
    private void Update()
    {
        if (triggering && GOLight.GetComponent<Light>() && Input.GetKeyDown(KeyCode.Space))  //Keycode can be changed
        {
            if (GOLight.activeInHierarchy)
                GOLight.SetActive(false);
            if (!GOLight.activeInHierarchy)
                GOLight.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        triggering = true;
    }

    private void OnTriggerExit(Collider other)
    {
        triggering = false;
    }
}
