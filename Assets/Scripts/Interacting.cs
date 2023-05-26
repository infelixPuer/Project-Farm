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

    [SerializeField] 
    private Canvas _selectingCropCanvas;
    
    private GameObject _selectedObject;
    private Camera _cam;
    private PlayerMovement _playerMovement;
    
    private bool _isSelectingCrop;

    private void Awake()
    {
        _cam = GetComponentInChildren<Camera>();
        _playerMovement = FindObjectOfType<PlayerMovement>();
    }

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        if (!_isSelectingCrop)
        {
            ChooseInteractionOption();
            Interact();
        }
        
        ShowSelectingCropUI();
    }

    private static void ChooseInteractionOption()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            InteractionManager.Instance.UpdatePlayerActionState(InteractionState.MakeSeedbed);

        if (Input.GetKeyDown(KeyCode.Alpha2))
            InteractionManager.Instance.UpdatePlayerActionState(InteractionState.Plant);

        if (Input.GetKeyDown(KeyCode.Alpha3))
            InteractionManager.Instance.UpdatePlayerActionState(InteractionState.Water);
    }

    private void Interact()
    {
        if (!Input.GetMouseButtonDown(0)) return;
        
        switch (InteractionManager.Instance.interaction)
        {
            case InteractionState.MakeSeedbed:
                MakeSeedbed();
                break;
            case InteractionState.Plant:
                Plant();
                break;
            case InteractionState.Water:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void Plant()
    {
        var ray = new Ray(_cam.transform.position, _cam.transform.forward);
        Physics.Raycast(ray, out var hitInfo, _interactionDistance, _plantLayer);
        Debug.DrawRay(_cam.transform.position, _cam.transform.forward, Color.green);

        if (hitInfo.collider == null) return;
        
        var seedbed = hitInfo.collider.GetComponentInParent<Seedbed>();

        if (seedbed == null) return;

        if (seedbed.State != SeedbedState.Empty) return;
        
        seedbed.UpdateTileState();
        seedbed.SetCrop(InteractionManager.Instance.crop, seedbed);
    }

    private void MakeSeedbed()
    {
        var ray = new Ray(_cam.transform.position, _cam.transform.forward);
        Physics.Raycast(ray, out var hitInfo, _interactionDistance, _selectionLayer.value);
        Debug.DrawRay(_cam.transform.position, _cam.transform.forward, Color.green);

        if (hitInfo.collider != null)
        {
            WorldMap.Instance.InstantiateSeedbed(hitInfo.point);
        }
    }

    private void ShowSelectingCropUI()
    {
        _isSelectingCrop = Input.GetKey(KeyCode.Q);
        
        _selectingCropCanvas.gameObject.SetActive(_isSelectingCrop);
        Cursor.lockState = _isSelectingCrop ? CursorLockMode.Confined : CursorLockMode.Locked;
        Cursor.visible = _isSelectingCrop;
        _playerMovement.enabled = !_isSelectingCrop;
    }
}
