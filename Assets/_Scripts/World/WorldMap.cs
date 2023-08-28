using UnityEngine;

namespace _Scripts.World
{
    public class WorldMap : MonoBehaviour
    {
        [SerializeField]
        private int _worldWidth;

        [SerializeField]
        private int _worldHeight;

        [SerializeField]
        [Range(1, 10)]
        private int _cellSizeInUnityUnit;

        [SerializeField]
        private Seedbed _seedbedPrefab;

        [SerializeField]
        private GameObject _debugObjectPrefab;

        public Grid Grid { get; private set; }

        public static WorldMap Instance;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(Instance);
            }
            else
            {
                Destroy(this);
            }

            Grid = new Grid(_worldWidth, _worldHeight, _cellSizeInUnityUnit);
            Grid.CreateGridObjects(_debugObjectPrefab.transform);
        }

        public void SetTileAtGridPosition(GridPosition gridPosition, Tile tile)
        {
            var gridObject = Grid.GetGridObject(gridPosition);
            gridObject.Tile = tile;
        }

        public void InstantiateSeedbed(Vector3 interactionPoint)
        {
            var gridObject = Grid.GetGridObject(GetGridPosition(interactionPoint));

            if (gridObject == null) return;

            if (gridObject.State != GridObjectState.Empty)
            {
                Debug.LogWarning("Spot is already occupied!");
                return;
            }

            var seedbed = Instantiate(_seedbedPrefab, GetWorldPosition(GetGridPosition(interactionPoint)), Quaternion.identity);
            var model = seedbed.GetComponentInChildren<MeshRenderer>()?.gameObject;

            model!.transform.localScale = GetLocalScale(model.transform);
            seedbed.Parent = gridObject;
            gridObject.Tile = seedbed;
            gridObject.State = GridObjectState.Occupied;
            SeedbedManager.Instance.AddSeedbed(seedbed);
        }

        public void RemoveSeedbed(Seedbed seedbed)
        {
            seedbed.Parent.State = GridObjectState.Empty;
            seedbed.Parent.Tile = null;
            Destroy(seedbed.gameObject);
            SeedbedManager.Instance.RemoveSeedbed(seedbed);
        }

        public GridPosition GetGridPosition(Vector3 pos) => Grid.GetGridPosition(pos);

        public Vector3 GetWorldPosition(GridPosition gridPosition) =>
            new(gridPosition.X * _cellSizeInUnityUnit + 0.5f * _cellSizeInUnityUnit, 0f, gridPosition.Z * _cellSizeInUnityUnit + 0.5f * _cellSizeInUnityUnit);

        public Vector3 GetLocalScale(Transform gameObjectTransform) =>
            new(gameObjectTransform.localScale.x * _cellSizeInUnityUnit, gameObjectTransform.localScale.y, gameObjectTransform.localScale.z * _cellSizeInUnityUnit);
    }
}
