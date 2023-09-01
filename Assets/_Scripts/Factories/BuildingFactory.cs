using _Scripts.ConstructionBuildings;
using _Scripts.World;
using UnityEngine;

namespace _Scripts.Factories
{
    public class BuildingFactory : TileFactory
    {
        [SerializeField]
        private ConstructionBuilding _buildingPrefab;
        
        public static BuildingFactory Instance;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else 
                Destroy(gameObject);
        }

        public override Tile CreateTile(Transform parent, Vector3 position)
        {
            var building = Instantiate(_buildingPrefab, parent);
            building.SetObjectOpaque();

            return building;
        }

        public override Tile RecreateTile(Transform parent, Vector3 position, TileDTO tile)
        {
            throw new System.NotImplementedException();
        }
    }
}