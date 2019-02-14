using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerFocus : MonoBehaviour {
    //Goes on the volume to trigger
    public int ToTrigger = 0;
    public bool Replay = false;
    private bool triggered = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter(Collider other)
    {
        if (!triggered)
        {
            GameObject.FindGameObjectWithTag("LevelControl").GetComponent<FocusCamera>().TriggerSwitch(ToTrigger);
            triggered = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (triggered && Replay)
            triggered = false;
    }
}
