using System.Collections.Generic;
using _Scripts.SaveAndLoad;
using _Scripts.Services;
using UnityEngine;

namespace _Scripts.World
{
    public class WorldMap : MonoBehaviour, IDataHandler
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

        public IDataService DataService = new JsonDataService();

        private bool _isInitialized;

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
        }

        public void Init()
        {
            if (_isInitialized)
            {
                Grid = null;
                foreach (Transform child in transform)
                    Destroy(child.gameObject);
            }
            
            Grid = new Grid(_worldWidth, _worldHeight, _cellSizeInUnityUnit);
            Grid.CreateGridDebugObjects(_debugObjectPrefab.transform);
            _isInitialized = true;
        }

        public void Init(WorldMapDTO worldMapDTO)
        {
            if (_isInitialized)
            {
                Grid = null;
                foreach (Transform child in transform)
                    Destroy(child.gameObject);
            }
            
            _worldWidth = worldMapDTO.Grid.Width;
            _worldHeight = worldMapDTO.Grid.Height;
            _cellSizeInUnityUnit = 2;
            Grid = new Grid(worldMapDTO.Grid);
            Grid.CreateGridDebugObjects(_debugObjectPrefab.transform);
            _isInitialized = true;
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

        public void SaveData()
        {
            if (!_isInitialized)
            {
                Debug.LogError("Unable to save due to object not instatiated!");
                return;
            }
            
            var worldMapDTO = new WorldMapDTO()
            {
                Grid = new GridDTO()
                {
                    Width = Grid.GridObjects.GetLength(0),
                    Height = Grid.GridObjects.GetLength(1),
                    GridObjects = ConvertToList(Grid.GridObjects)
                }
            };

            if (DataService.SaveData(worldMapDTO, "world-map.json"))
                Debug.Log("Data was saved!");
        }

        public void LoadData()
        {
            if (_isInitialized)
            {
                Grid = null;
                foreach (Transform child in transform)
                    Destroy(child.gameObject);
            }
            
            var worldMapDTO = DataService.LoadData<WorldMapDTO>("world-map.json");
            
            if (worldMapDTO is not null)
                Debug.Log("Data was loaded!");
            
            Init(worldMapDTO);
        }

        private List<GridObjectDTO> ConvertToList(GridObject[,] array)
        {
            var list = new List<GridObjectDTO>();

            for (int i = 0; i < array.GetLength(0); i++)
                for (int j = 0; j < array.GetLength(1); j++)
                    list.Add(new GridObjectDTO()
                    {
                        GridPosition = new GridPositionDTO()
                        {
                            X = array[i, j].GridPosition.X,
                            Y = array[i, j].GridPosition.Z,
                        }
                    });

            return list;
        }
    }
}
