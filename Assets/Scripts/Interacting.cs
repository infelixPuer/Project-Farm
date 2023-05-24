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
    private GameObject _seedbedPrefab;
    
    private GameObject _selectedObject;
    private Camera _cam;

    private void Awake()
    {
        _cam = GetComponentInChildren<Camera>();
    }

    private void Update()
    {
        if (Input.GetKeyDown("1"))
            PlayerManager.Instance.UpdatePlayerActionState(PlayerActionState.MakeSeedbed);
        
        if (Input.GetKeyDown("2"))
            PlayerManager.Instance.UpdatePlayerActionState(PlayerActionState.Plant);
        
        if (Input.GetMouseButtonDown(0))
            Interact();
            
        
        
        // var seedbed = Instantiate(_seedbedPrefab, WorldMap.Instance.GetWorldPosition(WorldMap.Instance.GetGridPosition(hitInfo.point)), Quaternion.identity);
        // seedbed.transform.localScale = WorldMap.Instance.GetLocalScale(seedbed.transform);

        // _selectedObject = hitInfo.collider.gameObject;
        // Debug.Log(WorldMap.Instance.GetGridPosition(_selectedObject.transform.position));
        // _selectedObject.GetComponent<Seedbed>().UpdateCellState();
    }

    private void Interact()
    {
        MakeSeedbed(PlayerManager.Instance.PlayerAction);
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
