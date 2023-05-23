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
    [SerializeField] 
    private Material _emptyMaterial;
    
    [SerializeField] 
    private Material _plantedMaterial;
    
    [SerializeField] 
    private Material _wateredMaterial;
    
    [SerializeField] 
    private Material _readyToHarvestMaterial;

    private MeshRenderer _renderer;

    public CellState State => _state;

    private CellState _state;

    private void Awake()
    {
        _renderer = GetComponent<MeshRenderer>();
    }

    private void Start()
    {
        WorldMap.Instance.SetCellAtGridPosition(WorldMap.Instance.GetGridPosition(transform.position), this);
        _state = CellState.Empty;
        UpdateCellMaterial();
    }

    public void UpdateCellState()
    {
        if (_state == CellState.ReadyToHarvest)
            _state = CellState.Empty;
        else 
            _state++;

        UpdateCellMaterial();
    }

    private void UpdateCellMaterial()
    {
        _renderer.material = _state switch
        {
            CellState.Empty => _emptyMaterial,
            CellState.Planted => _plantedMaterial,
            CellState.Watered => _wateredMaterial,
            CellState.ReadyToHarvest => _readyToHarvestMaterial,
            _ => _emptyMaterial
        };
    }
}
