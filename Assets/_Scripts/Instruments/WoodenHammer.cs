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
        
        public ConstructionBuilding Building 
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
                    
                    _buildingInstance = Instantiate(_building, _cam.transform.position, Quaternion.identity);
                    _buildingInstance.SetObjectTransparent();
                    _buildingInstance.ToogleColliderState(true);
                }
            }
        }
        
        private ConstructionBuilding _building;
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
                var buildingPos = _buildingInstance.transform.position;
                buildingPos = camTransform.position + camTransform.forward * 4;
                buildingPos.y = Mathf.Clamp(buildingPos.y, _buildingInstance.transform.localScale.y * 0.5f, buildingPos.y);
                _buildingInstance.transform.position = buildingPos;

                var gridPos = _grid.GetGridPosition(buildingPos);
                var gridObj = _grid.GetGridObject(gridPos);

                if (gridObj is not null)
                {
                    _buildingInstance.transform.position = _grid.GetWorldPosition(gridPos);
                }
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