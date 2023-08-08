using _Scripts.ConstructionBuildings;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace _Scripts.UI
{
    public class ConstructionBuildingItemUI : MonoBehaviour
    {
        [Header("Visuals")] 
        [SerializeField] 
        private Image _image;

        [SerializeField] 
        private TextMeshProUGUI _woodPrice;
        
        [SerializeField] 
        private TextMeshProUGUI _stonePrice;
        
        [Header("Functionallity")]
        [SerializeField]       
        private Button _button;
                               
        public ConstructionBuildingSO BuildingData { get; private set; }
        
        
        public ConstructionBuildingItemUI Init(ConstructionBuildingSO buildingData)
        {
            BuildingData = buildingData;
            _image.sprite = BuildingData.Sprite;
            _woodPrice.text = BuildingData.WoodCost.ToString();
            _stonePrice.text = BuildingData.StoneCost.ToString();
            
            return this;
        }
        
        public void SetButtonAction(UnityAction action) => _button.onClick.AddListener(action);
    }
}