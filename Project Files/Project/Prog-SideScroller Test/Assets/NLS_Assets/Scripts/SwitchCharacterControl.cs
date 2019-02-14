using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCharacterControl : MonoBehaviour {
    [Header("No more than one of these can be present in a level.")]
    [Header("Put in master control entity.")]    
    public GameObject player1;
    public GameObject player2;
    GameObject mainCamera;
    CameraFollow cFollow;

    PlayerMove p1Move;
    PlayerMove p2Move;

    public bool onPlayer1 = true;

    // Use this for initialization
    void Start () {
        p1Move = player1.GetComponent<PlayerMove>();
        p2Move = player2.GetComponent<PlayerMove>();
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        cFollow = mainCamera.GetComponent<CameraFollow>();

        p2Move.enabled = false;

        Physics.IgnoreCollision(player1.GetComponent<BoxCollider>(), player2.GetComponent<BoxCollider>());
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.T) && !GetComponent<FocusCamera>().focusing)
        {
            p1Move.enabled = !p1Move.enabled;
            p2Move.enabled = !p2Move.enabled;
            onPlayer1 = !onPlayer1;

            if (onPlayer1)
            {
                cFollow.target = player1.transform;
                player2.GetComponent<Rigidbody>().Sleep();
                p2Move.enabled = false;
                p2Move.animator.enabled = false;

                p1Move.enabled = true;
                p1Move.animator.enabled = true;

            }
            else if (!onPlayer1)
            {
                cFollow.target = player2.transform;
                player1.GetComponent<Rigidbody>().Sleep();
                p1Move.enabled = false;
                p1Move.animator.enabled = false;

                p2Move.enabled = true;
                p2Move.animator.enabled = true;
            }
        }
	}
}
