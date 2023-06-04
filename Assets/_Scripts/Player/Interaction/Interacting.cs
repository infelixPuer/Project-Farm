using System;
using UnityEngine;

public class Interacting : MonoBehaviour
{
    [SerializeField] 
    private Canvas _selectingCropCanvas;

    [SerializeField] 
    private Canvas _inventoryCanvas;
    
    private PlayerMovement _playerMovement;
    private Action _interactionAction;

    private void Awake()
    {
        _playerMovement = FindObjectOfType<PlayerMovement>();
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
            Interact();
        }
        
        ShowSelectingCropUI();
        ShowInventory();
    }

    private void ChooseInteractionOption()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            InteractionManager.Instance.UpdatePlayerActionState(InteractionState.MakingSeedbed, this);

        if (Input.GetKeyDown(KeyCode.Alpha2))
            InteractionManager.Instance.UpdatePlayerActionState(InteractionState.Planting, this);
        
        if (Input.GetKeyDown(KeyCode.Alpha4))
            InteractionManager.Instance.UpdatePlayerActionState(InteractionState.Watering, this);
    }

    public void OnInteractionAction(Action action)
    {
        _interactionAction = null;
        _interactionAction += action;
    }

    private void Interact()
    {
        if (!Input.GetMouseButtonDown(0)) return;
        
        _interactionAction?.Invoke(); 
    }
    
    // ReSharper disable once InconsistentNaming
    private void ShowSelectingCropUI()
    {
        if ((!Input.GetKey(KeyCode.Q) || InteractionManager.Instance.IsSelectingCrop) && (Input.GetKey(KeyCode.Q) || !InteractionManager.Instance.IsSelectingCrop)) return;

        InteractionManager.Instance.IsSelectingCrop = !InteractionManager.Instance.IsSelectingCrop;

        _selectingCropCanvas.gameObject.SetActive(InteractionManager.Instance.IsSelectingCrop);
        Cursor.lockState = InteractionManager.Instance.IsSelectingCrop ? CursorLockMode.Confined : CursorLockMode.Locked;
        Cursor.visible = InteractionManager.Instance.IsSelectingCrop;
        _playerMovement.enabled = !InteractionManager.Instance.IsSelectingCrop;
    }
    
    private void ShowInventory()
    {
            _inventoryCanvas.gameObject.SetActive(Input.GetKey(KeyCode.Tab));
    }
}
