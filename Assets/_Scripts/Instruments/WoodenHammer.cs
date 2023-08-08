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
                        Destroy(_buildingInstance);
                    }
                    
                    _building = value;
                    _buildingInstance = Instantiate(_building, _cam.transform.position, Quaternion.identity);
                    _buildingInstance.SetObjectTransparent();
                }
            }
        }
        
        private ConstructionBuilding _building;
        private ConstructionBuilding _buildingInstance;
        private Camera _cam;

        private void Awake()
        {
            _cam = InteractionManager.Instance.Cam;
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
        
        private IEnumerator CloseCanvas()
        {
            yield return new WaitUntil(() => Input.GetMouseButtonUp(1));
            UIManager.Instance.HideCanvas(_constructionBuildingLoaderCanvas);
        }
    }
}