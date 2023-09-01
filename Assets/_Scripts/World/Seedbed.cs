using System;
using _Scripts.Crops;
using _Scripts.Player.Inventory;
using _Scripts.UI;
using UnityEngine;

namespace _Scripts.World
{
    public class Seedbed : Tile
    {
        [Header("Materials")]
        [SerializeField]
        private Material _emptyMaterial;

        [SerializeField]
        private Material _plantedMaterial;

        [SerializeField]
        private Material _wateredMaterial;

        [Header("Seedbed Properties")]
        [SerializeField]
        private Tooltip _tooltip;
        
        [SerializeField]
        private GameObject _plantPlace;

        [SerializeField]
        private GameObject _seedbedModel;

        [SerializeField]
        private float _daysToDry;

        public float DaysToDry => _daysToDry;

        [SerializeField]
        [Range(0f, 1f)]
        private float _currentWaterLevel;

        public float CurrentWaterLevel => _currentWaterLevel;

        public GridObject Parent;

        private MeshRenderer _renderer;
        private CropBase _cropBase;

        private float _waterLevelAfterWatering;
        public float WaterLevelAfterWatering => _waterLevelAfterWatering;
        
        private TimeSpan _elapsedTime;
        public TimeSpan ElapsedTime => _elapsedTime;
        
        private DateTime _dateOfWatering;
        public DateTime DateOfWatering => _dateOfWatering;
        
        private bool _isWatered;
        public bool IsWatered => _isWatered;

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
            _tooltip.SetText($"{Math.Round(_currentWaterLevel * 100, 0)}%");
        }

        public void Init(SeedbedDTO seedbed)
        {
            _daysToDry = seedbed.DaysToDry;
            _currentWaterLevel = seedbed.CurrentWaterLevel;
            _waterLevelAfterWatering = seedbed.WaterLevelAfterWatering;
            _elapsedTime = seedbed.ElapsedTime;
            _dateOfWatering = seedbed.DateOfWatering;
            _isWatered = seedbed.IsWatered;
        }

        private void UpdateCellMaterial() => _renderer.material = State == TileState.Empty ? _emptyMaterial : _plantedMaterial;

        public override void UpdateTileState(TileState state) => State = state;

        public void PlantCrop(ItemSO crop)
        {
            crop.Object.TryGetComponent<Seed>(out var seed);

            var seedbedTransform = _seedbedModel.transform;

            var y = seedbedTransform!.position.y + seedbedTransform.localScale.y * 0.5f + seed.CropBase.transform.localScale.y * 0.5f;
            var plantPos = _plantPlace.transform.position;
            var cropObject = Instantiate(seed.CropBase, new Vector3(plantPos.x, y, plantPos.z), Quaternion.identity, transform);
            _cropBase = cropObject.GetComponent<CropBase>();
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

        private void UpdateWaterLevel()
        {
            if (!_isWatered) return;

            _elapsedTime = TimeManager.Instance.GetCurrentTime() - _dateOfWatering;
            var t = _elapsedTime / (TimeSpan.FromDays(_daysToDry) * _waterLevelAfterWatering);

            _currentWaterLevel = Mathf.Lerp(_waterLevelAfterWatering, 0f, (float)t);

            if (_currentWaterLevel <= 0f) DrySeedbed();
        }

        public float GetCurrentWaterLevel() => _currentWaterLevel;

        public void DrySeedbed()
        {
            _isWatered = false;
            _renderer.material = _emptyMaterial;
        }
        
        public void ShowTooltip() => _tooltip.Show();
        public void HideTooltip() => _tooltip.Hide();
    }
}
