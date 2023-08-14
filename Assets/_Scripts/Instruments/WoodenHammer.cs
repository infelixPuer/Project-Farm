using System;
using System.Collections;
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
        
        private ConstructionBuildingSO _building;
        private ConstructionBuilding _buildingInstance;
        private Grid _grid;
        private Camera _cam;

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
                var buildingPos = camTransform.position + camTransform.forward * 4;
                var scale = _buildingInstance.transform.localScale;
                buildingPos.y = Mathf.Clamp(buildingPos.y, scale.y * 0.5f, buildingPos.y);

                var gridPos = _grid.GetGridPosition(buildingPos);
                var gridObj = _grid.GetGridObject(gridPos);

                var gridPositions = _grid.GetGridPositions(_buildingInstance.transform);

                if (gridObj is not null)
                {
                    Vector3 position;
                    position = _grid.GetWorldPosition(gridPos);
                    position += Vector3.up * (scale.y * 0.5f);
                    buildingPos = position;
                }
                
                _buildingInstance.transform.position = buildingPos;
            }
        }

        public override void MainAction()
        {
            Debug.Log("Building!");
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
    }
}