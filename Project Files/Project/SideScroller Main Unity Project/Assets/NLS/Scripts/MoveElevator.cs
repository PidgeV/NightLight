using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveElevator : MonoBehaviour
{
    private Vector3 targetPosition;
    private float origin;
    private bool triggered = false;

    public GameObject objectToMove;
    public float speed = 0.1f;
    public float maxDis = 0;
    public float minDis = 0;

    // Use this for initialization
    void Start()
    {
        targetPosition = objectToMove.transform.position;
        origin = objectToMove.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.E) && triggered)
            if (objectToMove.transform.position.y <= origin + maxDis)
                targetPosition += new Vector3(0, speed, 0);

        if (Input.GetKey(KeyCode.Q) && triggered)
            if (objectToMove.transform.position.y >= origin + minDis)
                targetPosition += new Vector3(0, -speed, 0);

        objectToMove.transform.position = Vector3.Lerp(objectToMove.transform.position, targetPosition, speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player2")
        {
            triggered = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player2")
        {
            triggered = false;
        }
    }
}
