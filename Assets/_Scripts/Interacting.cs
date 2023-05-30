using System;
using UnityEngine;

public class Interacting : MonoBehaviour
{

    [SerializeField] 
    private Canvas _selectingCropCanvas;
    
    private PlayerMovement _playerMovement;
    private Action _interactionAction;
    
    private bool _isSelectingCrop;

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
        if (!_isSelectingCrop)
        {
            ChooseInteractionOption();
            Interact();
        }
        
        ShowSelectingCropUI();
    }

    private void ChooseInteractionOption()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            InteractionManager.Instance.UpdatePlayerActionState(InteractionState.MakingSeedbed, this);

        if (Input.GetKeyDown(KeyCode.Alpha2))
            InteractionManager.Instance.UpdatePlayerActionState(InteractionState.Planting, this);

        if (Input.GetKeyDown(KeyCode.Alpha3))
            InteractionManager.Instance.UpdatePlayerActionState(InteractionState.Growing, this);
    }

    public void OnInteractionAction(Action action)
    {
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
        if ((!Input.GetKey(KeyCode.Q) || _isSelectingCrop) && (Input.GetKey(KeyCode.Q) || !_isSelectingCrop)) return;

        _isSelectingCrop = !_isSelectingCrop;

        _selectingCropCanvas.gameObject.SetActive(_isSelectingCrop);
        Cursor.lockState = _isSelectingCrop ? CursorLockMode.Confined : CursorLockMode.Locked;
        Cursor.visible = _isSelectingCrop;
        _playerMovement.enabled = !_isSelectingCrop;
    }
}
