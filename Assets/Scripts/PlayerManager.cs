using System;
using UnityEngine;

public enum PlayerActionState
{
    MakeSeedbed,
    Plant,
    Water
}

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;

    public PlayerActionState PlayerAction;
    public event Action<PlayerActionState> OnPlayerActionStateChange; 

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
        UpdatePlayerActionState(PlayerActionState.MakeSeedbed);
    }

    public void UpdatePlayerActionState(PlayerActionState newState)
    {
        PlayerAction = newState;

        switch (newState)
        {
            case PlayerActionState.MakeSeedbed:
                HandleMakeSeedbed();
                break;
            case PlayerActionState.Plant:
                HandlePlant();
                break;
            case PlayerActionState.Water:
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
