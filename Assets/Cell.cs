using System;
using UnityEngine;

public enum CellState
{
    Empty,
    Planted,
    Watered,
    ReadyToHarvest
}

public class Cell : MonoBehaviour
{
    public CellState State
    {
        get => _state;
        set => _state = value;
    }

    private CellState _state;

    private void Start()
    {
        WorldMap.Instance.SetCellAtGridPosition(WorldMap.Instance.GetGridPosition(transform.position), this);
    }
}
