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

    private Grid _grid;
    
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

        for (int x = 0; x < _worldWidth; x++)
        {
            for (int z = 0; z < _worldHeight; z++)
            {
                var cell = Instantiate(_cellPrefab, new Vector3(x + 0.5f, 0, z + 0.5f) * _cellSizeInUnityUnit, Quaternion.identity, transform);
                cell.name = $"Cell: {x} {z}";
                cell.layer = gameObject.layer;
            }
        }
    }

    public GridPosition GetGridPosition(Vector3 pos)
    {
        return _grid.GetGridPosition(pos);
    }
}
