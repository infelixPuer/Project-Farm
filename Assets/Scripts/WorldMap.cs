using System;
using UnityEngine;

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
    private GameObject _seedbedPrefab;

    [SerializeField] 
    private GameObject _debugObjectPrefab;

    private Grid _grid;
    private GameObject[,] _cells;
    
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
        
        _grid = new Grid(_worldWidth, _worldHeight, _cellSizeInUnityUnit);
        _cells = new GameObject[_worldWidth, _worldHeight];

        // for (int x = 0; x < _worldWidth; x++)
        // {
        //     for (int z = 0; z < _worldHeight; z++)
        //     {
        //         _cells[x, z] = Instantiate(_cellPrefab, new Vector3(x + 0.5f, 0, z + 0.5f) * _cellSizeInUnityUnit, Quaternion.identity, transform);
        //         _cells[x, z].transform.localScale = new Vector3(_cells[x, z].transform.localScale.x * _cellSizeInUnityUnit, _cells[x, z].transform.localScale.y, _cells[x, z].transform.localScale.z * _cellSizeInUnityUnit);
        //         _cells[x, z].name = $"Cell: {x} {z}";
        //         _cells[x, z].layer = gameObject.layer;
        //     }
        // }
        
        _grid.CreateGridObjects(_debugObjectPrefab.transform);
    }

    public void SetTileAtGridPosition(GridPosition gridPosition, ITilable tile)
    {
        var gridObject = _grid.GetGridObject(gridPosition);
        gridObject.Tile = tile;
    }

    public void InstantiateSeedbed(Vector3 interactionPoint)
    {
        if (_grid.GetGridObject(GetGridPosition(interactionPoint)).State != GridObjectState.Empty)
        {
            Debug.LogWarning("Spot is already occupied!");
            return;
        }
        
        var seedbed = Instantiate(_seedbedPrefab, GetWorldPosition(GetGridPosition(interactionPoint)), Quaternion.identity);
        var model = seedbed.GetComponentInChildren<MeshRenderer>()?.gameObject;
        var gridObject = _grid.GetGridObject(GetGridPosition(interactionPoint));
        
        model!.transform.localScale = GetLocalScale(model.transform);
        seedbed.GetComponentInChildren<Seedbed>().Parent = gridObject;
        gridObject.State = GridObjectState.Occupied;
    }

    //public Seedbed GetCellAtGridPosition(GridPosition gridPosition) => _grid.GetGridObject(gridPosition).seedbed;

    public GridPosition GetGridPosition(Vector3 pos) => _grid.GetGridPosition(pos);

    public Vector3 GetWorldPosition(GridPosition gridPosition) => 
        new(gridPosition.X * _cellSizeInUnityUnit + 0.5f * _cellSizeInUnityUnit, 0f, gridPosition.Z * _cellSizeInUnityUnit + 0.5f * _cellSizeInUnityUnit);

    public Vector3 GetLocalScale(Transform gameObjectTransform) =>
        new(gameObjectTransform.localScale.x * _cellSizeInUnityUnit, gameObjectTransform.localScale.y, gameObjectTransform.localScale.z * _cellSizeInUnityUnit);
}
