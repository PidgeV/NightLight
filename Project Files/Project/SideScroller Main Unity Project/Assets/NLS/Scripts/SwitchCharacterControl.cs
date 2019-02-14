using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCharacterControl : MonoBehaviour {
    [Header("No more than one of these can be present in a level.")]
    [Header("Put in master control entity.")]    
    public GameObject yenno;
    public GameObject fen;
    GameObject mainCamera;
    CameraFollow cFollow;

    PlayerMove p1Move;
    PlayerMove p2Move;

    public bool onPlayer1 = true;

    // Use this for initialization
    void Start () {
        if (yenno == null) yenno = GameObject.FindGameObjectWithTag("Player");
        if (fen == null) fen = GameObject.FindGameObjectWithTag("Player2");

        p1Move = yenno.GetComponent<PlayerMove>();
        p2Move = fen.GetComponent<PlayerMove>();
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        cFollow = mainCamera.GetComponent<CameraFollow>();

        p2Move.enabled = false;

        Physics.IgnoreCollision(yenno.GetComponent<BoxCollider>(), fen.GetComponent<BoxCollider>());
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.T) && !GetComponent<FocusCamera>().focusing)
        {
            SwitchControl();
        }
	}

    public void SwitchControl()
    {
        p1Move.enabled = !p1Move.enabled;
        p2Move.enabled = !p2Move.enabled;
        onPlayer1 = !onPlayer1;

        if (onPlayer1)
        {
            cFollow.target = yenno.transform;
            fen.GetComponent<Rigidbody>().Sleep();
            p2Move.enabled = false;
            p2Move.animator.enabled = false;

            p1Move.enabled = true;
            p1Move.animator.enabled = true;

        }
        else if (!onPlayer1)
        {
            cFollow.target = fen.transform;
            yenno.GetComponent<Rigidbody>().Sleep();
            p1Move.enabled = false;
            p1Move.animator.enabled = false;

            p2Move.enabled = true;
            p2Move.animator.enabled = true;
        }
    }

    public static void EnableFen()
    {
        GameObject fen = GameObject.FindGameObjectWithTag("Player2");
        fen.SetActive(true);
    }

    //Global function to focus back to character
    public static void RetargetCamera()
    {
        SwitchCharacterControl swControl = GameObject.FindGameObjectWithTag("LevelControl").GetComponent<SwitchCharacterControl>();

        if (swControl.onPlayer1)
        {
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraFollow>().target = GameObject.FindGameObjectWithTag("Player").transform;
        }
        else if (!swControl.onPlayer1)
        {
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraFollow>().target = GameObject.FindGameObjectWithTag("Player2").transform;
        }
    }

    //Global function to  enable and disable Yenno
    public static bool YennoState
    {
        set
        {
            GameObject yenno = GameObject.FindGameObjectWithTag("Player");
            yenno.SetActive(value);
        }
    }

    //Global function to  enable and disable Yenno
    public static bool FenState
    {
        set
        {
            GameObject fen = GameObject.FindGameObjectWithTag("Player2");
            fen.SetActive(value);
        }
    }
}
