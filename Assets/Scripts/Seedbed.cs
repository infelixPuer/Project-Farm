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

    [SerializeField]
    private GameObject _plantPlace;

    public GridObject Parent;
    
    private MeshRenderer _renderer;

    public SeedbedState State => _state;

    private SeedbedState _state;
    private CropScriptableObject _crop;

    private void Awake()
    {
        _renderer = GetComponentInChildren<MeshRenderer>();
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

    public void SetCrop(CropScriptableObject crop, Seedbed seedbed)
    {
        _crop = crop;
        var mesh = GetComponentInChildren<MeshRenderer>();
        var meshTransform = mesh.gameObject.transform;
        var y = meshTransform.position.y + meshTransform.localScale.y * 0.5f + _crop.PhasesOfGrowing[0].transform.localScale.y * 0.5f;
        var plantPos = _plantPlace.transform.position;
        Instantiate(_crop.PhasesOfGrowing[0], new Vector3(plantPos.x, y, plantPos.z), Quaternion.identity, seedbed.gameObject.transform);
    }
}
