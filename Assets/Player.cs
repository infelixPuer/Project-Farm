using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Range(0f, 10f)]
    [SerializeField] 
    private float _interactionDistance = 3f;

    [SerializeField] 
    private Material _selectedMaterial;

    private Camera _cam;
    private float _multiplier = 200f;
    private GameObject _selectedObject;

    private void Awake()
    {
        _cam = GetComponentInChildren<Camera>();
    }

    private void Update()
    {
        if (Input.GetKey("w"))
            transform.position += 10f * Time.deltaTime * transform.forward;

        if (Input.GetKey("s"))
            transform.position += 10f * Time.deltaTime * -transform.forward;

        if (Input.GetKey("a"))
            transform.position += 10f * Time.deltaTime * -transform.right;

        if (Input.GetKey("d"))
            transform.position += 10f * Time.deltaTime * transform.right;

        var mouseX = Input.GetAxis("Mouse X");
        var mouseY = Input.GetAxis("Mouse Y");
        
        transform.Rotate(Vector3.up, mouseX * _multiplier * Time.deltaTime, Space.World);
        _cam.transform.Rotate(new Vector3(-1f, 0, 0), mouseY * _multiplier * Time.deltaTime, Space.Self);

        if (!Input.GetMouseButton(0)) return;

        var ray = new Ray(_cam.transform.position, _cam.transform.forward);
        Physics.Raycast(ray, out var hitInfo, _interactionDistance);
        Debug.DrawRay(_cam.transform.position, _cam.transform.forward, Color.green);

        if (hitInfo.collider == null) return;

        _selectedObject = hitInfo.collider.gameObject;
        Debug.Log($"Hit: {_selectedObject.name}");
        _selectedObject.GetComponent<MeshRenderer>().material = _selectedMaterial;
    }
}
