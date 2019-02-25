using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMultiFollow : MonoBehaviour
{
    public GameObject yenno;
    public GameObject fen;
    public float cameraSpeed = 10;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        SetCameraPos();
    }

    private void SetCameraPos()
    {
        Vector3 middle = Vector3.zero;
        middle += yenno.transform.position;
        middle += fen.transform.position;
        middle = middle / 2;

        float deltaX = yenno.transform.position.x - fen.transform.position.x;
        float deltaY = yenno.transform.position.y - fen.transform.position.y;
        float deltaZ = yenno.transform.position.z - fen.transform.position.z;

        middle.z = yenno.transform.position.z - (float)(Math.Sqrt(Math.Pow(deltaX, 2) + Math.Pow(deltaY, 2) + Math.Pow(deltaZ, 2)));
        //middle.z = objects

        transform.position = Vector3.MoveTowards(transform.position, middle, cameraSpeed * Time.deltaTime);
    }
}
