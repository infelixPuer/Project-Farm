using System;
using _Scripts.Instruments;
using UnityEngine;
using _Scripts.Player.Interaction;
using _Scripts.UI;
using _Scripts.World;

public class Interactor : MonoBehaviour
{
    [SerializeField] 
    private Canvas _inventoryCanvas;

    [SerializeField] 
    private Transform _itemPoint;
    public InstrumentBase ItemInHand { get; private set; }
    
    private Action _interactionAction;

    private Camera _cam;
    private bool _isInventoryOpened;

    private void Awake()
    {
        _cam = Camera.main;
    }

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        if (!InteractionManager.Instance.IsShowingUI)
        {
            if (Input.GetMouseButtonDown(0))
            {
                PerformMainActionOfItemInHand();
            }

            if (Input.GetMouseButtonDown(1))
            {
                PerformSecondaryActionOfItemInHand();
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                Interaction();
            }

            if (Input.GetKeyDown(KeyCode.X))
            {
                DropItemFromHand();
            }

            if (Input.GetKeyDown(KeyCode.C))
            {
                ShowSeedbedWaterLevel();
            }

            if (Input.GetKeyUp(KeyCode.C))
            {
                HideSeedbedWaterLevel();
            }
        }
        
        ShowInventory();
    }

    private void PerformMainActionOfItemInHand()
    {
        if (ItemInHand is null)
            return;
        
        ItemInHand.MainAction();
    }

    private void PerformSecondaryActionOfItemInHand()
    {
        if (ItemInHand is null)
            return;
        
        ItemInHand.SecondaryAction();
    }

    private void Interaction()
    { 
        var ray = new Ray(_cam.transform.position, _cam.transform.forward);

        if (Physics.Raycast(ray, out var hitInfo, 3f))
        {
            if (hitInfo.collider.gameObject.TryGetComponent(out IInteractable obj))
            {
                obj.Interact(this);
            }
        }
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

    public void GetItemInHand(InstrumentBase obj)
    {
        if (ItemInHand is not null)
            return;
        
        obj.transform.localPosition = _itemPoint.position;
        obj.transform.localRotation = _itemPoint.rotation;
        obj.transform.localScale = _itemPoint.localScale;
        obj.transform.SetParent(this.transform);
        obj.ResetObject(false);
        ItemInHand = obj;
    }

    public void DropItemFromHand()
    {
        if (ItemInHand is null)
            return;

        var obj = ItemInHand.gameObject;
        ItemInHand.ResetObject(true);
        ItemInHand = null;
        obj.transform.SetParent(null);
        obj.transform.position = _cam.transform.position + _cam.transform.forward * 2f;
        obj.transform.localScale = Vector3.one;
        obj.transform.rotation = Quaternion.identity;
    }

    private void ShowSeedbedWaterLevel()
    {
        if (SeedbedManager.Instance.IsTooltipVisible)
            return;
        
        var seedbeds = SeedbedManager.Instance.Seedbeds;
        
        if (seedbeds.Count == 0)
            return;

        foreach (var seedbed in seedbeds)
        {
            seedbed.ShowTooltip();
        }

        SeedbedManager.Instance.IsTooltipVisible = true;
    }

    private void HideSeedbedWaterLevel()
    {
        if (!SeedbedManager.Instance.IsTooltipVisible)
            return;
        
        var seedbeds = SeedbedManager.Instance.Seedbeds;
        
        if (seedbeds.Count == 0)
            return;

        foreach (var seedbed in seedbeds)
        {
            seedbed.HideTooltip();
        }
        
        SeedbedManager.Instance.IsTooltipVisible = false;
    }
}
