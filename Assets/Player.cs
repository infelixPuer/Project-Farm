using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Camera _cam;
    private float _multiplier = 200f;

    private void Awake()
    {
        _cam = GetComponentInChildren<Camera>();
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

        var mouseX = Input.GetAxis("Mouse X");
        var mouseY = Input.GetAxis("Mouse Y");
        
        transform.Rotate(Vector3.up, mouseX * _multiplier * Time.deltaTime, Space.World);
        _cam.transform.Rotate(new Vector3(-1f, 0, 0), mouseY * _multiplier * Time.deltaTime, Space.Self);

        if (Input.GetMouseButtonDown(0))
        {
            var ray = new Ray(_cam.transform.position, _cam.transform.forward);
            Physics.Raycast(ray, out RaycastHit hitInfo, 2f);
            Debug.DrawRay(_cam.transform.position, _cam.transform.forward, Color.green);
            Debug.Log(hitInfo.collider?.name ?? "No hits");
        }
    }
}
