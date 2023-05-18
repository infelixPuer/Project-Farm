using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Vector3 _lastMousePos;
    private float _multiplier = 200f;

    private void Start()
    {
        _lastMousePos = Input.mousePosition;
    }

    private void Update()
    {
        if (Input.GetKey("w"))
            transform.position += transform.forward * Time.deltaTime * 10f;

        if (Input.GetKey("s"))
            transform.position += -transform.forward * Time.deltaTime * 10f;

        if (Input.GetKey("a"))
            transform.position += -transform.right * Time.deltaTime * 10f;

        if (Input.GetKey("d"))
            transform.position += transform.right * Time.deltaTime * 10f;

        // var mouseX = Input.GetAxis("Mouse X");
        //
        // transform.Rotate(Vector3.up, mouseX * _multiplier * Time.deltaTime, Space.World);
        
        Vector2 mouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        // Rotate the object based on the mouse delta
        transform.Rotate(Vector3.up, mouseDelta.x * _multiplier * Time.deltaTime, Space.World);
        transform.Rotate(Vector3.left, mouseDelta.y * _multiplier * Time.deltaTime, Space.World);
    }
}
