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
            InteractionManager.Instance.UpdatePlayerActionState(InteractionState.Grow);
    }

    private void Interact()
    {
        if (!Input.GetMouseButtonDown(0)) return;
        
        switch (InteractionManager.Instance.Interaction)
        {
            case InteractionState.MakeSeedbed:
                MakeSeedbed();
                break;
            case InteractionState.Plant:
                Plant();
                break;
            case InteractionState.Grow:
                Grow();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
    
    private void MakeSeedbed()
    {
        var ray = new Ray(_cam.transform.position, _cam.transform.forward);
        var hasHit = Physics.Raycast(ray, out var hitInfo, _interactionDistance, _selectionLayer.value);

        if (!hasHit) return;

        WorldMap.Instance.InstantiateSeedbed(hitInfo.point);
    }
    
    private void Plant()
    {
        if (!InteractionManager.Instance.IsCropSelected)
        {
            Debug.LogWarning("Crop is not selected!");
            return;
        }
        
        var ray = new Ray(_cam.transform.position, _cam.transform.forward);
        var hasHit = Physics.Raycast(ray, out var hitInfo, _interactionDistance, _plantLayer);

        if (!hasHit) return;

        var seedbed = hitInfo.collider.GetComponentInParent<Seedbed>();

        if (seedbed == null || seedbed.State != TileState.Empty) return;

        seedbed.UpdateTileState(TileState.Occupied);
        seedbed.PlantCrop(InteractionManager.Instance.Crop);
    }

    private void Grow()
    {
        var ray = new Ray(_cam.transform.position, _cam.transform.forward);
        var hasHit = Physics.Raycast(ray, out var hitInfo, _interactionDistance, _plantLayer.value);

        if (!hasHit) return;

        var seedbed = hitInfo.collider.GetComponentInParent<Seedbed>();
        seedbed.GetCrop().Grow();
    }

    // ReSharper disable once InconsistentNaming
    private void ShowSelectingCropUI()
    {
        if ((!Input.GetKey(KeyCode.Q) || _isSelectingCrop) && (Input.GetKey(KeyCode.Q) || !_isSelectingCrop)) return;

        _isSelectingCrop = !_isSelectingCrop;

        _selectingCropCanvas.gameObject.SetActive(_isSelectingCrop);
        Cursor.lockState = _isSelectingCrop ? CursorLockMode.Confined : CursorLockMode.Locked;
        Cursor.visible = _isSelectingCrop;
        _playerMovement.enabled = !_isSelectingCrop;
    }
}
