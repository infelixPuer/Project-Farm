using System;
using UnityEngine;

public enum InteractionState
{
    MakeSeedbed,
    Plant,
    Water
}

public class InteractionManager : MonoBehaviour
{
    public static InteractionManager Instance;

    public InteractionState interaction;
    public event Action<InteractionState> OnPlayerActionStateChange; 

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
        UpdatePlayerActionState(InteractionState.MakeSeedbed);
    }

    public void UpdatePlayerActionState(InteractionState newState)
    {
        interaction = newState;

        switch (newState)
        {
            case InteractionState.MakeSeedbed:
                HandleMakeSeedbed();
                break;
            case InteractionState.Plant:
                HandlePlant();
                break;
            case InteractionState.Water:
                HandleWater();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }
        
        OnPlayerActionStateChange?.Invoke(newState);
    }

    private void HandleMakeSeedbed()
    {
           
    }

    private void HandlePlant()
    {
        
    }
    
    private void HandleWater()
    {
        
    }
}
