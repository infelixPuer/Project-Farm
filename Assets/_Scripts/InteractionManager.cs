using System;
using UnityEngine;
using UnityEngine.Serialization;

public enum InteractionState
{
    MakingSeedbed,
    Planting,
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

    private Camera _cam;
    
    public static InteractionManager Instance;

    public InteractionState interactionState;
    public event Action<InteractionState> OnPlayerActionStateChange;

    public CropScriptableObject Crop;
    public bool IsCropSelected;

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
    }

    private void Update()
    {
        IsCropSelected = Crop != null;
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
    }

    private void HandlePlanting(Interacting interacting)
    {
        interacting.OnInteractionAction(Plant);
    }
    
    private void HandleGrowing(Interacting interacting)
    {
        interacting.OnInteractionAction(Grow);
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
        seedbed.PlantCrop(Crop);
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
