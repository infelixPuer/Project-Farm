using UnityEngine;

public class WorldMap : MonoBehaviour
{
    [SerializeField] 
    private int _worldWidth;

    [SerializeField] 
    private int _worldHeight;

    [SerializeField] 
    private int _cellSizeInUnityUnit;

    [SerializeField] 
    private GameObject _cellPrefab;

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

        for (int x = 0; x < _worldWidth; x++)
        {
            for (int z = 0; z < _worldHeight; z++)
            {
                _cells[x, z] = Instantiate(_cellPrefab, new Vector3(x + 0.5f, 0, z + 0.5f) * _cellSizeInUnityUnit, Quaternion.identity, transform);
                _cells[x, z].transform.localScale = new Vector3(_cells[x, z].transform.localScale.x * _cellSizeInUnityUnit, _cells[x, z].transform.localScale.y, _cells[x, z].transform.localScale.z * _cellSizeInUnityUnit);
                _cells[x, z].name = $"Cell: {x} {z}";
                _cells[x, z].layer = gameObject.layer;
            }
        }
        
        _grid.CreateGridObjects(_debugObjectPrefab.transform);
    }

    public void SetCellAtGridPosition(GridPosition gridPosition, Cell cell)
    {
        var gridObject = _grid.GetGridObject(gridPosition);
        gridObject.Cell = cell;
    }

    public Cell GetCellAtGridPosition(GridPosition gridPosition) => _grid.GetGridObject(gridPosition).Cell;

    public GridPosition GetGridPosition(Vector3 pos) => _grid.GetGridPosition(pos);
}
