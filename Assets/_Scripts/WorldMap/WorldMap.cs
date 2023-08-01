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
        _grid.CreateGridObjects(_debugObjectPrefab.transform);
    }

    public void SetTileAtGridPosition(GridPosition gridPosition, Tile tile)
    {
        var gridObject = _grid.GetGridObject(gridPosition);
        gridObject.Tile = tile;
    }

    public void InstantiateSeedbed(Vector3 interactionPoint)
    {
        var gridObject = _grid.GetGridObject(GetGridPosition(interactionPoint));

        if (gridObject == null) return;
        
        if (gridObject.State != GridObjectState.Empty)
        {
            Debug.LogWarning("Spot is already occupied!");
            return;
        }
        
        var seedbed = Instantiate(_seedbedPrefab, GetWorldPosition(GetGridPosition(interactionPoint)), Quaternion.identity);
        var model = seedbed.GetComponentInChildren<MeshRenderer>()?.gameObject;
        
        model!.transform.localScale = GetLocalScale(model.transform);
        seedbed.GetComponentInChildren<Seedbed>().Parent = gridObject;
        gridObject.State = GridObjectState.Occupied;
    }

    public void RemoveSeedbed(Seedbed seedbed)
    {
        seedbed.Parent.State = GridObjectState.Empty;
        Destroy(seedbed.gameObject);
    }

    public GridPosition GetGridPosition(Vector3 pos) => _grid.GetGridPosition(pos);

    public Vector3 GetWorldPosition(GridPosition gridPosition) => 
        new(gridPosition.X * _cellSizeInUnityUnit + 0.5f * _cellSizeInUnityUnit, 0f, gridPosition.Z * _cellSizeInUnityUnit + 0.5f * _cellSizeInUnityUnit);

    public Vector3 GetLocalScale(Transform gameObjectTransform) =>
        new(gameObjectTransform.localScale.x * _cellSizeInUnityUnit, gameObjectTransform.localScale.y, gameObjectTransform.localScale.z * _cellSizeInUnityUnit);
}
