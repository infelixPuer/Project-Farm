using System;
using _Scripts.ConstructionBuildings;
using _Scripts.Instruments;
using UnityEngine;

namespace _Scripts.UI
{
    public class ConstructionBuildingLoaderUI : MonoBehaviour
    {
        [SerializeField] 
        private GameObject _contentPanel;
        
        [SerializeField]
        private ConstructionBuildingItemUI _constructionBuildingItemUIPrefab;

        [SerializeField]
        private Interactor _interactor;

        private void OnEnable()
        {
            var buildings = Resources.LoadAll<ConstructionBuildingSO>("Scriptables/ConstructionBuildings");
            
            var woodenHammer = _interactor.ItemInHand as WoodenHammer;
            
            foreach (var building in buildings)
            {
                var buildingItem = Instantiate(_constructionBuildingItemUIPrefab, _contentPanel.transform);
                buildingItem.Init(building);
                buildingItem.SetButtonAction(() => woodenHammer.Building = building);
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