using _Scripts.Player.Inventory;
using UnityEngine;

namespace _Scripts.ConstructionBuildings
{
    [CreateAssetMenu(fileName = "New construction building", menuName = "SOs/Construction building", order = 0)]
    public class ConstructionBuildingSO : ScriptableObject
    {
        [Header("Size")]
        public int Width;
        public int Depth;
        
        [Header("Item reference")]
        public ItemSO _woodItem;
        public ItemSO _stoneItem;
        
        [Header("Cost")]
        public int WoodCost;
        public int StoneCost;
        
        [Header("Visuals")]
        public ConstructionBuilding BuildingPrefab;
        public Sprite Sprite;
    }
}