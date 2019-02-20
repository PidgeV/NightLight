using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentTrigger : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" || other.tag == "Player2")
        {
            other.transform.parent = this.gameObject.transform;
            Debug.Log("Parented character");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player" || other.tag == "Player2")
        {
            other.transform.parent = null;
            Debug.Log("Un-parented character");
        }
    }
}
