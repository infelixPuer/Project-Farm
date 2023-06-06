using _Scripts.Player.Inventory;
using UnityEngine;

public class Seedbed : Tile
{
    [SerializeField] 
    private Material _emptyMaterial;
    
    [SerializeField] 
    private Material _plantedMaterial;

    [SerializeField] 
    private Material _wateredMaterial;

    [SerializeField]
    private GameObject _plantPlace;

    [SerializeField] 
    private GameObject _seedbedModel;

    [SerializeField] 
    private GameObject _cropPrefab;

    public GridObject Parent;
    private MeshRenderer _renderer;
    private ItemSO _cropSO;
    private Crop _crop;

    private bool _isWatered;

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

    public void PlantCrop(ItemSO crop)
    {
        _cropSO = crop;

        var seedbedTransform = _seedbedModel.transform;

        var y = seedbedTransform!.position.y + seedbedTransform.localScale.y * 0.5f + _cropSO.Object.transform.localScale.y * 0.5f;
        var plantPos = _plantPlace.transform.position;
        var cropGameObject = Instantiate(_cropPrefab, new Vector3(plantPos.x, y, plantPos.z), Quaternion.identity, transform);
        _crop = cropGameObject.GetComponent<Crop>();
        _crop.SetParentSeedbed(this);
    }

    public Crop GetCrop() => _crop;

    public void WaterSeedbed()
    {
        _isWatered = true;
        _renderer.material = _wateredMaterial;
    }

    public void DrySeedbed()
    {
        _isWatered = false;
        _renderer.material = _plantedMaterial;
    }

    public bool GetWateredStatus()
    {
        return _isWatered;
    }
}
