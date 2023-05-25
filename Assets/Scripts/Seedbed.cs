using UnityEngine;

public enum SeedbedState
{
    Empty,
    Planted
}

public class Seedbed : MonoBehaviour, ITilable
{
    [SerializeField] 
    private Material _emptyMaterial;
    
    [SerializeField] 
    private Material _plantedMaterial;

    private MeshRenderer _renderer;

    public SeedbedState State => _state;

    private SeedbedState _state;

    private void Awake()
    {
        _renderer = GetComponent<MeshRenderer>();
    }

    private void Start()
    {
        WorldMap.Instance.SetTileAtGridPosition(WorldMap.Instance.GetGridPosition(transform.position), this);
        _state = SeedbedState.Empty;
        UpdateCellMaterial();
    }

    private void UpdateCellMaterial()
    {
        _renderer.material = _state == SeedbedState.Empty ? _emptyMaterial : _plantedMaterial;
    }

    public void UpdateTileState()
    {
        _state = _state == SeedbedState.Empty ? SeedbedState.Planted : SeedbedState.Empty;

        UpdateCellMaterial();
    }
}
