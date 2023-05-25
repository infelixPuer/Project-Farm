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

    [SerializeField] 
    private LayerMask _plantLayer;

    [SerializeField] 
    private GameObject _seedbedPrefab;
    
    private GameObject _selectedObject;
    private Camera _cam;

    private void Awake()
    {
        _cam = GetComponentInChildren<Camera>();
    }

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            InteractionManager.Instance.UpdatePlayerActionState(InteractionState.MakeSeedbed);
        
        if (Input.GetKeyDown(KeyCode.Alpha2))
            InteractionManager.Instance.UpdatePlayerActionState(InteractionState.Plant);
        
        if (Input.GetKeyDown(KeyCode.Alpha3))
            InteractionManager.Instance.UpdatePlayerActionState(InteractionState.Water);
        
        if (Input.GetMouseButtonDown(0))
            Interact();
    }

    private void Interact()
    {
        MakeSeedbed(InteractionManager.Instance.interaction);
        Plant(InteractionManager.Instance.interaction);
    }

    private void Plant(InteractionState state)
    {
        if (state != InteractionState.Plant) return;
        
        var ray = new Ray(_cam.transform.position, _cam.transform.forward);
        Physics.Raycast(ray, out var hitInfo, _interactionDistance, _plantLayer);
        Debug.DrawRay(_cam.transform.position, _cam.transform.forward, Color.green);

        if (hitInfo.collider != null)
            hitInfo.collider.GetComponent<Seedbed>()?.UpdateTileState();
    }

    private void MakeSeedbed(InteractionState state)
    {
        if (state != InteractionState.MakeSeedbed) return;
        
        var ray = new Ray(_cam.transform.position, _cam.transform.forward);
        Physics.Raycast(ray, out var hitInfo, _interactionDistance, _selectionLayer.value);
        Debug.DrawRay(_cam.transform.position, _cam.transform.forward, Color.green);

        if (hitInfo.collider != null)
            WorldMap.Instance.InstantiateSeedbed(hitInfo.point);
    }
}
