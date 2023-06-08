using System;
using _Scripts.Crops;
using _Scripts.Player.Inventory;
using UnityEngine;
using UnityEngine.Serialization;

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
    private int _daysToDry;

    [SerializeField]
    [Range(0f, 1f)]
    private float _currentWaterLevel;
    
    public GridObject Parent;
    
    private MeshRenderer _renderer;
    private ItemSO _cropSO;
    private Crop _crop;
    private CropBase _cropBase;

    private float _waterLevelAfterWatering;
    private TimeSpan _elapsedTime;
    private DateTime _dateOfWatering;
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

    private void Update()
    {
        UpdateWaterLevel();
    }

    private void UpdateCellMaterial()
    {
        _renderer.material = State == TileState.Empty ? _emptyMaterial : _plantedMaterial;
    }

    public override void UpdateTileState(TileState state)
    {
        State = state;

        //UpdateCellMaterial();
    }           

    public void PlantCrop(ItemSO crop)
    {
        _cropSO = crop;

        var seedbedTransform = _seedbedModel.transform;

        var y = seedbedTransform!.position.y + seedbedTransform.localScale.y * 0.5f + _cropSO.Object.transform.localScale.y * 0.5f;
        var plantPos = _plantPlace.transform.position;
        var cropGameObject = Instantiate(crop.Object, new Vector3(plantPos.x, y, plantPos.z), Quaternion.identity, transform);
        _cropBase = cropGameObject.GetComponent<CropBase>();
        _cropBase.SetParentSeedbed(this);
    }

    public void WaterSeedbed()
    {
        _isWatered = true;
        _dateOfWatering = TimeManager.Instance.GetCurrentTime();
        _currentWaterLevel = _currentWaterLevel + 0.5f >= 1f ? 1f : _currentWaterLevel + 0.5f;
        _waterLevelAfterWatering = _currentWaterLevel;
        _renderer.material = _wateredMaterial;
    }

    public void UpdateWaterLevel()
    {
        if (!_isWatered) return;
        
        _elapsedTime = TimeManager.Instance.GetCurrentTime() - _dateOfWatering;
        var t = _elapsedTime / TimeSpan.FromDays(_daysToDry) / _waterLevelAfterWatering;
        
        _currentWaterLevel = Mathf.Lerp(_waterLevelAfterWatering, 0f, (float)t);
        
        if (_currentWaterLevel <= 0f) DrySeedbed();
    }

    public void DrySeedbed()
    {
        _isWatered = false;
        _renderer.material = _emptyMaterial;
    }

    public bool GetWateredStatus()
    {
        return _isWatered;
    }
}
