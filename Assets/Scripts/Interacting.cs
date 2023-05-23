using System;
using UnityEngine;

public class Interacting : MonoBehaviour
{
    [Range(0f, 10f)]
    [SerializeField] 
    private float _interactionDistance;

    [SerializeField] 
    private Material _selectedMaterial;

    [SerializeField] 
    private LayerMask _selectionLayer;
    
    private GameObject _selectedObject;
    private Camera _cam;

    private void Awake()
    {
        _cam = GetComponentInChildren<Camera>();
    }

    private void Update()
    {
        if (!Input.GetMouseButtonDown(0)) return;

        var ray = new Ray(_cam.transform.position, _cam.transform.forward);
        Physics.Raycast(ray, out var hitInfo, _interactionDistance, _selectionLayer.value);
        Debug.DrawRay(_cam.transform.position, _cam.transform.forward, Color.green);

        if (hitInfo.collider == null)
            return;
        
        _selectedObject = hitInfo.collider.gameObject;
        Debug.Log(WorldMap.Instance.GetGridPosition(_selectedObject.transform.position));
        _selectedObject.GetComponent<Cell>().UpdateCellState();
    }
}
