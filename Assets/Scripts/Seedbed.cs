using UnityEngine;

public enum SeedbedState
{
    Empty,
    Planted,
    Watered,
    ReadyToHarvest
}

public class Seedbed : MonoBehaviour
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

    public SeedbedState State => _state;

    private SeedbedState _state;

    private void Awake()
    {
        _renderer = GetComponent<MeshRenderer>();
    }

    private void Start()
    {
        WorldMap.Instance.SetCellAtGridPosition(WorldMap.Instance.GetGridPosition(transform.position), this);
        _state = SeedbedState.Empty;
        UpdateCellMaterial();
    }

    public void UpdateCellState()
    {
        if (_state == SeedbedState.ReadyToHarvest)
            _state = SeedbedState.Empty;
        else 
            _state++;

        UpdateCellMaterial();
    }

    private void UpdateCellMaterial()
    {
        _renderer.material = _state switch
        {
            SeedbedState.Empty => _emptyMaterial,
            SeedbedState.Planted => _plantedMaterial,
            SeedbedState.Watered => _wateredMaterial,
            SeedbedState.ReadyToHarvest => _readyToHarvestMaterial,
            _ => _emptyMaterial
        };
    }
}
