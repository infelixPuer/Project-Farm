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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            PlayerManager.Instance.UpdatePlayerActionState(PlayerActionState.MakeSeedbed);
        
        if (Input.GetKeyDown(KeyCode.Alpha2))
            PlayerManager.Instance.UpdatePlayerActionState(PlayerActionState.Plant);
        
        if (Input.GetKeyDown(KeyCode.Alpha3))
            PlayerManager.Instance.UpdatePlayerActionState(PlayerActionState.Water);
        
        if (Input.GetMouseButtonDown(0))
            Interact();
    }

    private void Interact()
    {
        MakeSeedbed(PlayerManager.Instance.PlayerAction);
        Plant(PlayerManager.Instance.PlayerAction);
    }

    private void Plant(PlayerActionState state)
    {
        if (state != PlayerActionState.Plant) return;
        
        var ray = new Ray(_cam.transform.position, _cam.transform.forward);
        Physics.Raycast(ray, out var hitInfo, _interactionDistance, _plantLayer);
        Debug.DrawRay(_cam.transform.position, _cam.transform.forward, Color.green);

        if (hitInfo.collider != null)
            hitInfo.collider.GetComponent<Seedbed>()?.UpdateSeedbedState();
    }

    private void MakeSeedbed(PlayerActionState state)
    {
        if (state != PlayerActionState.MakeSeedbed) return;
        
        var ray = new Ray(_cam.transform.position, _cam.transform.forward);
        Physics.Raycast(ray, out var hitInfo, _interactionDistance, _selectionLayer.value);
        Debug.DrawRay(_cam.transform.position, _cam.transform.forward, Color.green);

        if (hitInfo.collider != null)
            WorldMap.Instance.InstantiateSeedbed(hitInfo.point);
    }
}
