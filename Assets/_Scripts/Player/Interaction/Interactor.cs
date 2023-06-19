using System;
using UnityEngine;
using _Scripts.Player.Interaction;
using _Scripts.UI;

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
        if (!InteractionManager.Instance.IsSelectingSeed)
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
        if (Input.GetKeyUp(KeyCode.Q))
        {
            UIManager.Instance.HideCanvas(_selectingCropCanvas);
        }
        
        if (!Input.GetKeyDown(KeyCode.Q)) return;
        
        UIManager.Instance.ShowCanvas(_selectingCropCanvas);
    }
    
    private void ShowInventory()
    {
        if (Input.GetKeyUp(KeyCode.Tab))
        {
            UIManager.Instance.HideCanvas(_inventoryCanvas);
        }
        
        if (!Input.GetKeyDown(KeyCode.Tab)) return;
        
        UIManager.Instance.ShowCanvas(_inventoryCanvas);
    }
}
