using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinCameraS : MonoBehaviour
{
    public Transform target;
    private Vector3 offset;

    [Range(0.01f, 1.0f)]
    public float smooth = 0.5f;

    public bool LookAtTarget = false;
    public bool RotateArountTarget = true;

    public float RotationSpeed = 5.0f;

    private bool update = false;

    // Use this for initialization
    void Start()
    {
        offset = transform.position - target.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (RotateArountTarget)
            {
                Quaternion camTurnAngle = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * RotationSpeed, Vector3.up);

                offset = camTurnAngle * offset;
            }
            Vector3 newPos = target.position + offset;
            transform.position = Vector3.Slerp(transform.position, newPos, smooth);

            if (LookAtTarget || RotateArountTarget)
                transform.LookAt(target);
        }
    }
}
