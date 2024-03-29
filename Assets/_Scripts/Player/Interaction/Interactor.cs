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
    private Canvas _saveAndLoadCanvas;
    
    [SerializeField]
    private InstrumentSlotsContainer instrumentSlotsContainer;

    [SerializeField] 
    private Transform _itemPoint;
    public InstrumentBase ItemInHand { get; private set; }
    
    private Action _interactionAction;

    private Camera _cam;
    private bool _isInventoryOpened;

    public static Interactor Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else 
            Destroy(gameObject);
        
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
                PerformMainActionOfItemInHand();

            if (Input.GetKeyDown(KeyCode.T))
                PerformSecondaryActionOfItemInHand();

            if (Input.GetKeyDown(KeyCode.E))
                Interaction();

            if (Input.GetKeyDown(KeyCode.X))
                DropItemFromHand();

            if (Input.GetKeyDown(KeyCode.V))
                ShowSeedbedWaterLevel();

            if (Input.GetKeyUp(KeyCode.V))
                HideSeedbedWaterLevel();
            
            GetNumsInput();
        }

        ShowSaveAndLoadCanvas();
        ShowInventory();
    }

    private void ShowSaveAndLoadCanvas()
    {
        if (Input.GetKeyDown(KeyCode.C))
            UIManager.Instance.ShowCanvas(_saveAndLoadCanvas);
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
            if (hitInfo.collider.gameObject.TryGetComponent(out IInteractable obj))
                obj.Interact(this);
    }
    
    private void ShowInventory()
    {
        if (Input.GetKeyUp(KeyCode.Tab))
            UIManager.Instance.HideCanvas(_inventoryCanvas);
        
        if (!Input.GetKeyDown(KeyCode.Tab)) return;
        
        UIManager.Instance.ShowCanvas(_inventoryCanvas);
    }

    public void GetItemInHand(InstrumentBase instrument)
    {
        if (ItemInHand is not null)
            return;

        var instrumentTransform = instrument.transform;
        instrumentTransform.localPosition = _itemPoint.position;
        instrumentTransform.localRotation = _itemPoint.rotation;
        instrumentTransform.localScale = _itemPoint.localScale;
        instrument.transform.SetParent(this.transform);
        instrument.ResetObject(false);
        ItemInHand = instrument;
    }

    public InstrumentBase DropItemFromHand()
    {
        if (ItemInHand is null)
            return null;

        var obj = ItemInHand;
        ItemInHand.ResetObject(true);
        ItemInHand = null;
        obj.transform.SetParent(null);
        obj.transform.position = _cam.transform.position + _cam.transform.forward * 2f;
        obj.transform.localScale = Vector3.one;
        obj.transform.rotation = Quaternion.identity;

        return obj;
    }

    public void ResetItemInHand() => ItemInHand = null;

    private void ShowSeedbedWaterLevel()
    {
        if (SeedbedManager.Instance.IsTooltipVisible)
            return;
        
        var seedbeds = SeedbedManager.Instance.Seedbeds;
        
        if (seedbeds.Count == 0)
            return;

        foreach (var seedbed in seedbeds)
            seedbed.ShowTooltip();

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
            seedbed.HideTooltip();
        
        SeedbedManager.Instance.IsTooltipVisible = false;
    }

    private void GetNumsInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            instrumentSlotsContainer.SelectSlot(0);
        
        if (Input.GetKeyDown(KeyCode.Alpha2))
            instrumentSlotsContainer.SelectSlot(1);
        
        if (Input.GetKeyDown(KeyCode.Alpha3))
            instrumentSlotsContainer.SelectSlot(2);
        
        if (Input.GetKeyDown(KeyCode.Alpha4))
            instrumentSlotsContainer.SelectSlot(3);
        
        if (Input.GetKeyDown(KeyCode.Alpha5))
            instrumentSlotsContainer.SelectSlot(4);
        
        if (Input.GetKeyDown(KeyCode.Alpha6))
            instrumentSlotsContainer.SelectSlot(5);
        
        if (Input.GetKeyDown(KeyCode.Alpha7))
            instrumentSlotsContainer.SelectSlot(6);
    }
}
