using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public enum InteractionState
{
    MakingSeedbed,
    Planting,
    Watering,
    Growing
}

public class InteractionManager : MonoBehaviour
{
    [Range(0f, 10f)]
    [SerializeField] 
    private float _interactionDistance;

    [SerializeField] 
    private LayerMask _selectionLayer;

    [SerializeField] 
    private LayerMask _plantLayer;

    [SerializeField] 
    private GameObject _seedbedPrefab;

    [SerializeField] 
    private TextMeshProUGUI _interactionText;

    private Camera _cam;
    
    public static InteractionManager Instance;

    public InteractionState interactionState;
    public event Action<InteractionState> OnPlayerActionStateChange;

    public CropScriptableObject SelectedCrop;
    public bool IsCropSelected;

    private List<CropScriptableObject> _crops;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }
        
        _cam = Camera.main;
        _crops = Resources.LoadAll<CropScriptableObject>("Scriptables/Crops").ToList();
    }

    private void Update()
    {
        IsCropSelected = SelectedCrop != null;
    }

    private void Start()
    {
        UpdatePlayerActionState(InteractionState.MakingSeedbed);
    }

    public void UpdatePlayerActionState(InteractionState newState)
    {
        interactionState = newState;

        switch (newState)
        {
            case InteractionState.MakingSeedbed:
                break;
            case InteractionState.Planting:
                break;
            case InteractionState.Watering:
                break;
            case InteractionState.Growing:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }
        
        OnPlayerActionStateChange?.Invoke(newState);
    }
    
    public void UpdatePlayerActionState(InteractionState newState, Interacting interacting)
    {
        interactionState = newState;

        switch (newState)
        {
            case InteractionState.MakingSeedbed:
                HandleMakingSeedbed(interacting);
                break;
            case InteractionState.Planting:
                HandlePlanting(interacting);
                break;
            case InteractionState.Watering:
                HandleWatering(interacting);
                break;
            case InteractionState.Growing:
                HandleGrowing(interacting);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }

        OnPlayerActionStateChange?.Invoke(newState);
    }

    private void HandleMakingSeedbed(Interacting interacting)
    {
        interacting.OnInteractionAction(MakeSeedbed);
        _interactionText.text = "Interaction action: Making seedbeds";
    }

    private void HandlePlanting(Interacting interacting)
    {
        interacting.OnInteractionAction(Plant);
        _interactionText.text = "Interaction action: Planting";
    }
    
    private void HandleWatering(Interacting interacting)
    {
        interacting.OnInteractionAction(Water);
        _interactionText.text = "Interaction action: Watering";
    }

    private void HandleGrowing(Interacting interacting)
    {
        interacting.OnInteractionAction(Grow);
        _interactionText.text = "Interaction action: Growing";
    }

    private void MakeSeedbed()
    {
        if (interactionState != InteractionState.MakingSeedbed) return;
        
        var ray = new Ray(_cam.transform.position, _cam.transform.forward);
        var hasHit = Physics.Raycast(ray, out var hitInfo, _interactionDistance, _selectionLayer.value);

        if (!hasHit) return;

        WorldMap.Instance.InstantiateSeedbed(hitInfo.point);
    }
    
    private void Plant()
    {
        if (interactionState != InteractionState.Planting) return;
        
        if (!IsCropSelected)
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
        seedbed.PlantCrop(SelectedCrop);
    }

    private void Water()
    {
        if (interactionState != InteractionState.Watering) return;
        
        var ray = new Ray(_cam.transform.position, _cam.transform.forward);
        var hasHit = Physics.Raycast(ray, out var hitInfo, _interactionDistance, _plantLayer);

        if (!hasHit) return;
        
        var seedbed = hitInfo.collider.GetComponentInParent<Seedbed>();
        
        if (seedbed.State != TileState.Occupied) return;
        
        seedbed.WaterSeedbed();
    }

    private void Grow()
    {
        if (interactionState != InteractionState.Growing) return;
        
        var ray = new Ray(_cam.transform.position, _cam.transform.forward);
        var hasHit = Physics.Raycast(ray, out var hitInfo, _interactionDistance, _plantLayer.value);

        if (!hasHit) return;

        var seedbed = hitInfo.collider.GetComponentInParent<Seedbed>();
        seedbed.GetCrop().Grow();
    }

    
}
