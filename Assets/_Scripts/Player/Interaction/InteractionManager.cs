using System;
using _Scripts.Player.Inventory;
using TMPro;
using UnityEngine;

public enum InteractionState
{
    MakingSeedbed,
    Planting,
    Watering
}

public class InteractionManager : MonoBehaviour
{
    [SerializeField] 
    private TextMeshProUGUI _interactionText;
    
    public static InteractionManager Instance;

    public InteractionState interactionState;
    public event Action<InteractionState> OnPlayerActionStateChange;

    public ItemSO SelectedSeed;
    public bool IsSeedSelected => SelectedSeed != null;
    public bool IsSelectingSeed;

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
    }

    private void Start()
    {
        UpdatePlayerInteractionState(InteractionState.MakingSeedbed);
    }

    public void UpdatePlayerInteractionState(InteractionState newState)
    {
        interactionState = newState;

        switch (newState)
        {
            case InteractionState.MakingSeedbed:
                _interactionText.text = "Interaction action: Making seedbeds";
                break;
            case InteractionState.Planting:
                _interactionText.text = "Interaction action: Planting";
                break;
            case InteractionState.Watering:
                _interactionText.text = "Interaction action: Watering";
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }
        
        OnPlayerActionStateChange?.Invoke(newState);
    }
}
