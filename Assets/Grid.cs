using UnityEngine;

public class Grid
{
    private int _width;
    private int _height;
    private float _cellSizeInUnityUnits;

    public Grid(int width, int height, float cellSizeInUnityUnits)
    {
        _width = width;
        _height = height;
        _cellSizeInUnityUnits = cellSizeInUnityUnits;
        
        for (int x = 0; x < _width; x++)
        {
            for (int z = 0; z < _height; z++)
            {
                Debug.DrawLine(new Vector3(x * _cellSizeInUnityUnits, 0, z * _cellSizeInUnityUnits), new Vector3(x * _cellSizeInUnityUnits, 0, (z + 1) * _cellSizeInUnityUnits), Color.red, 100f);
                Debug.DrawLine(new Vector3(x * _cellSizeInUnityUnits, 0, (z + 1) * _cellSizeInUnityUnits), new Vector3((x + 1) * _cellSizeInUnityUnits, 0, (z + 1) * _cellSizeInUnityUnits), Color.red, 100f);
                Debug.DrawLine(new Vector3((x + 1) * _cellSizeInUnityUnits, 0, (z + 1) * _cellSizeInUnityUnits), new Vector3((x + 1) * _cellSizeInUnityUnits, 0, z * _cellSizeInUnityUnits), Color.red, 100f);
                Debug.DrawLine(new Vector3((x + 1) * _cellSizeInUnityUnits, 0, z * _cellSizeInUnityUnits), new Vector3(x * _cellSizeInUnityUnits, 0, z * _cellSizeInUnityUnits), Color.red, 100f);
            }
        }
    }

    public GridPosition GetGridPosition(Vector3 position) => 
        new(Mathf.RoundToInt(position.x / _cellSizeInUnityUnits), 
            Mathf.RoundToInt(position.z / _cellSizeInUnityUnits));
}
