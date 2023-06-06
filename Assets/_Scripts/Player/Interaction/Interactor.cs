using System;
using UnityEngine;
using _Scripts.Player.Interaction;

public class Interactor : MonoBehaviour
{
    [SerializeField] 
    private Canvas _selectingCropCanvas;

    [SerializeField] 
    private Canvas _inventoryCanvas;
    
    private PlayerMovement _playerMovement;
    private Action _interactionAction;

    private Camera _cam;
    private bool _isInventoryOpened;

    private void Awake()
    {
        _playerMovement = FindObjectOfType<PlayerMovement>();
        _cam = Camera.main;
    }

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        if (!InteractionManager.Instance.IsSelectingCrop)
        {
            ChooseInteractionOption();
            SpecialInteraction();

            if (Input.GetKeyDown(KeyCode.E))
            {
                SimpleInteraction();
            }
        }
        
        ShowSelectingCropUI();
        ShowInventory();
    }

    private void ChooseInteractionOption()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            InteractionManager.Instance.UpdatePlayerInteractionState(InteractionState.MakingSeedbed);

        if (Input.GetKeyDown(KeyCode.Alpha2))
            InteractionManager.Instance.UpdatePlayerInteractionState(InteractionState.Planting);
        
        if (Input.GetKeyDown(KeyCode.Alpha3))
            InteractionManager.Instance.UpdatePlayerInteractionState(InteractionState.Watering);
    }

    private void SpecialInteraction()
    {
        if (!Input.GetMouseButtonDown(0)) return;
        
        var ray = new Ray(_cam.transform.position, _cam.transform.forward);
        
        if (Physics.Raycast(ray, out var hitInfo, 3f))
        {
            if (hitInfo.collider.gameObject.TryGetComponent<IInteractable>(out var obj))
            {
                obj.Interact(hitInfo);
            }
        }
    }

    private void SimpleInteraction()
    {
        var ray = new Ray(_cam.transform.position, _cam.transform.forward);

        if (Physics.Raycast(ray, out var hitInfo, 3f))
        {
            if (hitInfo.collider.gameObject.TryGetComponent(out IInteractable obj))
            {
                obj.Interact();
            }
        }
    }
    
    // ReSharper disable once InconsistentNaming
    private void ShowSelectingCropUI()
    {
        if ((!Input.GetKey(KeyCode.Q) || InteractionManager.Instance.IsSelectingCrop) && (Input.GetKey(KeyCode.Q) || !InteractionManager.Instance.IsSelectingCrop)) return;

        InteractionManager.Instance.IsSelectingCrop = !InteractionManager.Instance.IsSelectingCrop;
        TimeManager.Instance.TimeBlocked = InteractionManager.Instance.IsSelectingCrop;

        _selectingCropCanvas.gameObject.SetActive(InteractionManager.Instance.IsSelectingCrop);
        Cursor.lockState = InteractionManager.Instance.IsSelectingCrop ? CursorLockMode.Confined : CursorLockMode.Locked;
        Cursor.visible = InteractionManager.Instance.IsSelectingCrop;
        _playerMovement.enabled = !InteractionManager.Instance.IsSelectingCrop;
    }
    
    private void ShowInventory()
    {
        if ((!Input.GetKey(KeyCode.Tab) || _isInventoryOpened) && (Input.GetKey(KeyCode.Tab) || !_isInventoryOpened))
            return;

        TimeManager.Instance.TimeBlocked = !_isInventoryOpened;
        _isInventoryOpened = !_isInventoryOpened;
        _inventoryCanvas.gameObject.SetActive(_isInventoryOpened);
        Cursor.lockState = _isInventoryOpened ? CursorLockMode.Confined : CursorLockMode.Locked;
        Cursor.visible = _isInventoryOpened;
        _playerMovement.enabled = !_isInventoryOpened;
    }
}
