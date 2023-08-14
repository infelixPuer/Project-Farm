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
                var buildingTransform = _buildingInstance.transform;
                var scale = buildingTransform.localScale;
                var dir = (buildingPos - camTransform.position).normalized;
                var oldRotation = buildingTransform.rotation;
                var newRotation = new Quaternion(oldRotation.x, Quaternion.LookRotation(dir).y, oldRotation.z, oldRotation.w);
                
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
                    
                    // TODO: Make building rotating while snapping to the grid, rotation must be only by 90 degrees
                    newRotation = Quaternion.identity;
                }
                
                buildingTransform.position = buildingPos;
                buildingTransform.rotation = newRotation;
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