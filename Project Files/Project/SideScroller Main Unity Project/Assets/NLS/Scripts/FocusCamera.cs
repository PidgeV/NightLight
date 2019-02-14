using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FocusCamera : MonoBehaviour {
    [Header("No more than one of these can be present in a level.")]
    [Header("Put in master control entity.")]
    public int foo;
    public GameObject player1;
    public GameObject player2;
    public GameObject[] focuses;
    public float focusTime = 2.0f;
    public Vector3 spaceToStop = new Vector3(1.0f, 0, 0);
    private float timeFocused = 0.0f;
    public bool focusing = false;
    private GameObject mainCamera;
    private CameraFollow cFollow;

    // Use this for initialization
    void Start () {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        cFollow = mainCamera.GetComponent<CameraFollow>();
    }
	
	// Update is called once per frame
	void Update () {
		if( focusing && timeFocused >= focusTime)
        {
            if(GetComponent<SwitchCharacterControl>().onPlayer1)
            {
                cFollow.target = player1.transform;
                player1.GetComponent<PlayerMove>().enabled = true;
                player1.GetComponent<PlayerMove>().animator.enabled = true;
            }
            else if (!GetComponent<SwitchCharacterControl>().onPlayer1)
            {
                cFollow.target = player2.transform;
                player2.GetComponent<PlayerMove>().enabled = true;
                player2.GetComponent<PlayerMove>().animator.enabled = true;
            }
            focusing = false;
        }
        else if (timeFocused <= focusTime)
            timeFocused += Time.deltaTime;
	}
    public void TriggerSwitch(int segmentOfArray)
    {
        if (segmentOfArray < focuses.Length)
        {
            if (focuses[segmentOfArray].transform)
            {
                focusing = true;
                if (GetComponent<SwitchCharacterControl>().onPlayer1)
                {
                    //player1.GetComponent<Rigidbody>().AddForce(Vector3.zero, ForceMode.VelocityChange);
                    //player1.GetComponent<CharacterMotor>().ManageSpeed(-10, 0, true);
                    //player1.GetComponent<CharacterMotor>().MoveTo(player1.transform.position + spaceToStop, 5.0f, 0.2f, true);
                    player1.GetComponent<Rigidbody>().Sleep();
                    player1.GetComponent<PlayerMove>().enabled = false;
                    player1.GetComponent<PlayerMove>().animator.enabled = false;
                }
                else if (!GetComponent<SwitchCharacterControl>().onPlayer1)
                {
                    player2.GetComponent<Rigidbody>().Sleep();
                    player2.GetComponent<PlayerMove>().enabled = false;
                    player2.GetComponent<PlayerMove>().animator.enabled = false;
                }
                cFollow.target = focuses[segmentOfArray].transform;
                timeFocused = 0.0f;
            }
        }
    }
}
