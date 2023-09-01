using _Scripts.World;
using UnityEngine;

namespace _Scripts.Factories
{
    public class SeedbedFactory : TileFactory
    {
        [SerializeField]
        private Seedbed _seedbedPrefab;
        
        public static SeedbedFactory Instance;

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
            var map = WorldMap.Instance;
            
            var gridObject = map.Grid.GetGridObject(map.GetGridPosition(position));

            if (gridObject == null) return null;

            if (gridObject.State != GridObjectState.Empty)
            {
                Debug.LogWarning("Spot is already occupied!");
                return null;
            }

            var seedbed = Instantiate(_seedbedPrefab, map.GetWorldPosition(map.GetGridPosition(position)), Quaternion.identity);
            seedbed.transform.SetParent(parent);
            var model = seedbed.gameObject;

            model.transform.localScale = map.GetLocalScale(model.transform);
            seedbed.Parent = gridObject;
            gridObject.Tile = seedbed;
            gridObject.State = GridObjectState.Occupied;
            SeedbedManager.Instance.AddSeedbed(seedbed);

            return seedbed;
        }

        public override Tile RecreateTile(Transform parent, Vector3 position, TileDTO tile)
        {
            var map = WorldMap.Instance;
            
            var gridObject = map.Grid.GetGridObject(map.GetGridPosition(position));

            if (gridObject == null) return null;

            if (gridObject.State != GridObjectState.Empty)
            {
                Debug.LogWarning("Spot is already occupied!");
                return null;
            }

            var seedbed = Instantiate(_seedbedPrefab, map.GetWorldPosition(map.GetGridPosition(position)), Quaternion.identity);
            seedbed.transform.SetParent(parent);
            seedbed.Init((SeedbedDTO)tile);
            var model = seedbed.gameObject;

            model.transform.localScale = map.GetLocalScale(model.transform);
            seedbed.Parent = gridObject;
            gridObject.Tile = seedbed;
            gridObject.State = GridObjectState.Occupied;
            SeedbedManager.Instance.AddSeedbed(seedbed);

            return seedbed;
        }
    }
}