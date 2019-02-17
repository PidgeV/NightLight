using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTrain : MonoBehaviour
{
    private Vector3 targetPosition;
    private float origin;

    public float speed = 0.1f;
    public float maxDis = 0;
    public float minDis = 0;

    // Use this for initialization
    void Start()
    {
        targetPosition = this.gameObject.transform.position;
        origin = this.gameObject.transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.E))
            if (this.gameObject.transform.position.x <= origin + maxDis)
                targetPosition += new Vector3(speed, 0, 0);

        if (Input.GetKey(KeyCode.Q))
            if (this.gameObject.transform.position.x >= origin + minDis)
                targetPosition += new Vector3(-speed, 0, 0);

        this.gameObject.transform.position = Vector3.Lerp(this.gameObject.transform.position, targetPosition, speed);
    }
}
