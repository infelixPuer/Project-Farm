using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Camera _cam;
    private Vector3 _lastMousePos;
    private float _multiplier = 200f;

    private void Awake()
    {
        _cam = GetComponentInChildren<Camera>();
    }

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

        var mouseX = Input.GetAxis("Mouse X");
        var mouseY = Input.GetAxis("Mouse Y");

        // Rotate the object based on the mouse delta
        transform.Rotate(Vector3.up, mouseX * _multiplier * Time.deltaTime, Space.World);
        _cam.transform.Rotate(new Vector3(-1f, 0, 0), mouseY * _multiplier * Time.deltaTime, Space.Self);
    }
}
