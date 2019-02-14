//-----------------------------------------------------------------
//  This script should be put on Fen to help the teleport scripts
//  globally prevent teleporting immediately.
//-----------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportDelay : MonoBehaviour {
    [Header("This script should be put on Fen to help the teleport \n scripts globally prevent teleporting immediately.\n", order = 0)]
    [Space(20, order = 1)]

   [Tooltip("This is the time in seconds that Fen has to wait before teleporting.")]
    public float delay = 1;

    private float time;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
        if(time < delay)
        {
            time += Time.deltaTime;
        }
	}

    public bool CanTeleport() { return time >= delay; }
    public void ResetTime() { time = 0; }
}
