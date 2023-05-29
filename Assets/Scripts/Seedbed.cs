using UnityEngine;

public class Seedbed : Tile
{
    [SerializeField] 
    private Material _emptyMaterial;
    
    [SerializeField] 
    private Material _plantedMaterial;

    [SerializeField]
    private GameObject _plantPlace;

    [SerializeField] 
    private GameObject _seedbedModel;

    [SerializeField] 
    private GameObject _cropPrefab;

    public GridObject Parent;
    private MeshRenderer _renderer;
    private CropScriptableObject _crop;

    private void Awake()
    {
        _renderer = GetComponentInChildren<MeshRenderer>();
    }

    private void Start()
    {
        WorldMap.Instance.SetTileAtGridPosition(WorldMap.Instance.GetGridPosition(transform.position), this);
        State = TileState.Empty;
        UpdateCellMaterial();
    }

    private void UpdateCellMaterial()
    {
        _renderer.material = State == TileState.Empty ? _emptyMaterial : _plantedMaterial;
    }

    public override void UpdateTileState(TileState state)
    {
        State = state;

        UpdateCellMaterial();
    }           

    public void SetCrop(CropScriptableObject crop)
    {
        _crop = crop;

        var seedbedTransform = _seedbedModel.transform;

        var y = seedbedTransform!.position.y + seedbedTransform.localScale.y * 0.5f + _crop.PhasesOfGrowing[0].transform.localScale.y * 0.5f;
        var plantPos = _plantPlace.transform.position;
        Instantiate(_cropPrefab, new Vector3(plantPos.x, y, plantPos.z), Quaternion.identity, transform);
    }
}
