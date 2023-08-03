using _Scripts.Crops;
using _Scripts.Crops.CropStates;
using _Scripts.Player.Inventory;
using System;
using _Scripts.Helpers;
using _Scripts.Player.Interaction;
using UnityEngine;

public class CropStateMachine : MonoBehaviour, IInteractable
{
    [SerializeField]
    private CropBase _crop;
    
    public DateTime PlantedDate { get; set; }
    
    [HideInInspector]
    public bool IsReadyToHarvest;
    
    private CropBaseState _currentState;
    
    public CropGrowingState CropGrowingState = new();
    public CropReadyToHarvestState CropReadyToHarvestState = new();
    public CropSlowGrowingState CropSlowGrowingState = new();
    public CropWiltingState CropWiltingState = new();
    public CropDeadState CropDeadState = new();
    
    private ItemSO _seedSO;
    private ItemSO _cropSO;
    private PlayerInventory _inventory;

    private void Awake()
    {
        _seedSO = InteractionManager.Instance.SelectedSeed;
        _cropSO = _seedSO.Object.GetComponent<Seed>().CropBase.Item;
        PlantedDate = TimeManager.Instance.GetCurrentTime();
        _inventory = FindObjectOfType<PlayerInventory>();
    }

    private void Start()
    {
        _currentState = CropGrowingState;
        _currentState.Crop = _seedSO;
        _currentState.EnterCropState(this);
    }

    private void Update()
    {
        _currentState?.UpdateCropState(this);
    }
    
    public void Interact(Interactor interactor)
    {
        if (!IsReadyToHarvest) return;
            
        var initialItemCount = Mathf.RoundToInt(_crop.GetCropQuality() * _crop.Output);
        var itemCount = GaussianRandomNumberGenerator.GenerateRandomNumber(initialItemCount, 1f);

        if (!_inventory.Inventory.CanAddItem(new Item(_cropSO, (int)Math.Round(itemCount))))
        {
            Debug.Log("Inventory is full");
            return;
        }
            
        _inventory.AddItem(_cropSO, (int)Math.Round(itemCount));
        _crop.GetParentSeedbed().UpdateTileState(TileState.Empty);
        Destroy(_crop.gameObject);
    }

    public void TransitionToState(CropBaseState state)
    {
        _currentState = state;
        _currentState.EnterCropState(this);
    }
    
    public CropBase GetCrop() => _crop;
}
