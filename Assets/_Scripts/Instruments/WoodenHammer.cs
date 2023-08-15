using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using _Scripts.ConstructionBuildings;
using _Scripts.UI;

namespace _Scripts.Instruments
{
    public class WoodenHammer : InstrumentBase
    {
        [Header("Wooden hammer specifics")]
        [SerializeField]
        private Canvas _constructionBuildingLoaderCanvas;
        
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
                }
            }
        }

        private List<GridObject> _gridObjects;
        private ConstructionBuildingSO _building;
        private ConstructionBuilding _buildingInstance;
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

                var gridPos = _grid.GetGridPosition(buildingPos);
                var gridObj = _grid.GetGridObject(gridPos);

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

                        if (!_canBuild)
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

            foreach (var gridObject in _gridObjects)
            {
                gridObject.State = GridObjectState.Occupied;
            }
        }

        public override void SecondaryAction()
        {
            UIManager.Instance.ShowCanvas(_constructionBuildingLoaderCanvas);
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
            UIManager.Instance.HideCanvas(_constructionBuildingLoaderCanvas);
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
    }
}