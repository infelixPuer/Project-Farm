using UnityEngine;

namespace _Scripts.ConstructionBuildings
{
    [CreateAssetMenu(fileName = "New construction building", menuName = "SOs/Construction building", order = 0)]
    public class ConstructionBuildingSO : ScriptableObject
    {
        [Header("Size")]
        public int Width;
        public int Depth;
        
        [Header("Cost")]
        public int WoodCost;
        public int StoneCost;
        
        [Header("Visuals")]
        public ConstructionBuilding BuildingPrefab;
        public Sprite Sprite;

        // private void Awake()
        // {
        //     BuildingPrefab.Width = Width;
        //     BuildingPrefab.Depth = Depth;
        // }
    }
}