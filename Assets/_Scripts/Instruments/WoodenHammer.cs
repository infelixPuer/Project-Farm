using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using _Scripts.ConstructionBuildings;
using _Scripts.Player.Inventory;
using _Scripts.UI;
using _Scripts.World;
using UnityEngine.Serialization;
using Grid = _Scripts.World.Grid;

namespace _Scripts.Instruments
{
    public class WoodenHammer : InstrumentBase, ICanvasDependent
    {
        [FormerlySerializedAs("_constructionBuildingLoaderCanvas")]
        [Header("Wooden hammer specifics")]
        [SerializeField]
        private Canvas _selectingBuildingCanvas;
        
        [SerializeField]
        private float _distanceToBuilding = 6f;
        
        public ConstructionBuildingSO Building 
        { 
            private get => _building;
            set
            {
                if (!Equals(_building, value))
                {
                    if (_buildingInstance is not null)
                    {
                        Destroy(_buildingInstance.gameObject);
                        _buildingInstance = null;
                    }
                    
                    _building = value;
                    
                    if (_building is null)
                        return;
                    
                    _buildingInstance = Instantiate(_building.BuildingPrefab, _cam.transform.position, Quaternion.identity);
                    _buildingInstance.SetObjectScale(_building);
                    _buildingInstance.SetObjectTransparent();
                    _buildingInstance.ToogleColliderState(true);

                    _requiredItems[0] = new Item(_building._woodItem, _building.WoodCost);
                    _requiredItems[1] = new Item(_building._stoneItem, _building.StoneCost);
                }
            }
        }

        private List<GridObject> _gridObjects;
        private ConstructionBuildingSO _building;
        private ConstructionBuilding _buildingInstance;
        private Item[] _requiredItems = new Item[2];
        private Grid _grid;
        private Camera _cam;
        private bool _canBuild;

        private void Awake()
        {
            _cam = InteractionManager.Instance.Cam;
            
            _grid = WorldMap.Instance.Grid;
        }

        private void Update()
        {
            if (Building is not null)
            {
                var camTransform = _cam.transform;
                var scale = _buildingInstance.transform.localScale;
                var buildingPos = camTransform.position + camTransform.forward * _distanceToBuilding;
                
                buildingPos.y = Mathf.Clamp(buildingPos.y, scale.y * 0.5f, buildingPos.y);

                var gridPositions = _grid.GetGridPositions(buildingPos, scale);
                var gridObjects = gridPositions.Select(x => _grid.GetGridObject(x)).ToList();

                if (gridObjects.All(x => x is not null))
                {
                    if (gridObjects.All(x => x.State != GridObjectState.Occupied))
                    {
                        Vector3 position;
                        position = GetBuildingCenter(gridPositions);
                        position += Vector3.up * (scale.y * 0.5f);
                        buildingPos = position;
                        _gridObjects = gridObjects;

                        if (!_canBuild && PlayerInventory.Instance.CheckRequiredItems(_requiredItems))
                        {
                            _canBuild = true;
                        }
                    }
                }
                else
                {
                    _canBuild = false;
                }
                
                _buildingInstance.transform.position = buildingPos;
            }
        }

        public override void MainAction()
        {
            if (!_canBuild)
                return;
            
            var building = Instantiate(_buildingInstance, _buildingInstance.transform.position, Quaternion.identity);
            building.SetObjectOpaque();
            building.ToogleColliderState(false);
            
            RemoveRequiredItemsFromInventory();

            foreach (var gridObject in _gridObjects)
            {
                gridObject.State = GridObjectState.Occupied;
            }
        }

        public override void SecondaryAction()
        {
            UIManager.Instance.ShowCanvas(_selectingBuildingCanvas);
            StartCoroutine(CloseCanvas());
        }

        public override void ResetObject(bool gravityValue)
        {
            base.ResetObject(gravityValue);
            Building = null;
        }

        private IEnumerator CloseCanvas()
        {
            yield return new WaitUntil(() => Input.GetMouseButtonUp(1));
            UIManager.Instance.HideCanvas(_selectingBuildingCanvas);
        }

        private Vector3 GetBuildingCenter(List<GridPosition> positions)
        {
            var center = Vector3.zero;
            
            foreach (var position in positions)
            {
                center += _grid.GetWorldPosition(position);
            }
            
            center /= positions.Count;

            return center;
        }
        
        private void RemoveRequiredItemsFromInventory()
        {
            foreach (var item in _requiredItems)
            {
                PlayerInventory.Instance.RemoveItem(item.ItemData, item.Count);
            }
        }

        public void SetCanvas(Canvas canvas) => _selectingBuildingCanvas = canvas;
        public Canvas GetCanvas() => _selectingBuildingCanvas;
    }
}