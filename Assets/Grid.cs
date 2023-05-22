using UnityEngine;

public class Grid
{
    private int _width;
    private int _height;
    private float _cellSizeInUnityUnits;
    private GridObject[,] _gridObjectArray;

    public Grid(int width, int height, float cellSizeInUnityUnits)
    {
        _width = width;
        _height = height;
        _cellSizeInUnityUnits = cellSizeInUnityUnits;
        _gridObjectArray = new GridObject[width, height];
        
        for (int x = 0; x < _width; x++)
        {
            for (int z = 0; z < _height; z++)
            {
                _gridObjectArray[x, z] = new GridObject(this, new GridPosition(x, z));
            }
        }
    }

    public Vector3 GetWorldPosition(GridPosition gridPosition) => 
        new Vector3(gridPosition.X + 0.5f, 0, gridPosition.Z + 0.5f) * _cellSizeInUnityUnits;

    public GridPosition GetGridPosition(Vector3 position) => 
        new(Mathf.RoundToInt(position.x / _cellSizeInUnityUnits), 
            Mathf.RoundToInt(position.z / _cellSizeInUnityUnits));

    public void CreateGridObjects(Transform prefab)
    {
        foreach (var gridObject in _gridObjectArray)
        {
            GameObject.Instantiate(prefab, GetWorldPosition(gridObject.GridPosition), Quaternion.identity);
        }
    }
}
