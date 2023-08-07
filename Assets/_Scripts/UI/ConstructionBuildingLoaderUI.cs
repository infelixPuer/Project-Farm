using System;
using _Scripts.ConstructionBuildings;
using UnityEngine;

namespace _Scripts.UI
{
    public class ConstructionBuildingLoaderUI : MonoBehaviour
    {
        [SerializeField] 
        private GameObject _contentPanel;
        
        [SerializeField]
        private ConstructionBuildingItemUI _constructionBuildingItemUIPrefab;

        private void OnEnable()
        {
            var buildings = Resources.LoadAll<ConstructionBuildingSO>("Scriptables/ConstructionBuildings");
            
            foreach (var building in buildings)
            {
                var buildingItem = Instantiate(_constructionBuildingItemUIPrefab, _contentPanel.transform);
                buildingItem.Init(building);
            }
        }

        private void OnDisable()
        {
            foreach (Transform child in _contentPanel.transform)
            {
                Destroy(child.gameObject);
            }
        }
    }
}