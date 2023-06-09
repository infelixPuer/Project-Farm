using _Scripts.Crops;
using _Scripts.Crops.CropStates;
using _Scripts.Player.Interaction.InteractionTypes;
using _Scripts.Player.Inventory;
using System;
using UnityEngine;

public class CropStateMachine : MonoBehaviour
{
    [SerializeField] 
    private HarvestCrop _harvestCrop;
    
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

    private void Awake()
    {
        _seedSO = InteractionManager.Instance.SelectedSeed;
        _harvestCrop.Item = _seedSO.Object.GetComponent<Seed>().CropBase.Item;
        PlantedDate = TimeManager.Instance.GetCurrentTime();
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

    public void TransitionToState(CropBaseState state)
    {
        _currentState = state;
        _currentState.EnterCropState(this);
    }

    public HarvestCrop GetHarvestCrop()
    {
        _harvestCrop.Item = _seedSO;
       return _harvestCrop;
    }
    
    public CropBase GetCrop() => _crop;
}
