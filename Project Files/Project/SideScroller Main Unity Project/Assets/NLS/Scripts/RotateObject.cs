//------------------------------------------------------------------------------
//  Place on any object to rotate in the Z axis
//------------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour {
    public float degreesPerSecond = 30;
    public bool rotateConstantly;
    private PauseMenu shouldRotate;
    private int rotationDirection;
	// Use this for initialization
	void Start () {
        shouldRotate = GameObject.FindGameObjectWithTag("LevelControl").GetComponent<PauseMenu>();
	}
	
	// Update is called once per frame
	void Update () {

        if (!shouldRotate.isPaused)
        {
            if (rotateConstantly)
            {
                transform.Rotate(Vector3.down, degreesPerSecond * Mathf.PI / 180);
            }
            else
            {
                transform.Rotate(Vector3.down, (degreesPerSecond * Mathf.PI / 180) * rotationDirection);
            }
        }
	}

    //Used for changing through another script
    public void StartRotation() { rotationDirection = 1; }
    public void ReverseRotation() { rotationDirection *= -1; }
    public void StopRotation() { rotationDirection = 0; }
}
