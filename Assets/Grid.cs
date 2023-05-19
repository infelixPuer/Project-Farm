using UnityEngine;

public class Grid
{
    private int _gridWidth;
    private int _gridHeight;
    private float _cellSizeInUnityUnits;
    private int[,] _gridArray;

    public Grid(int gridWidth, int gridHeight, float cellSizeInUnityUnits)
    {
        _gridWidth = gridWidth;
        _gridHeight = gridHeight;
        _cellSizeInUnityUnits = cellSizeInUnityUnits;
        _gridArray = new int[_gridHeight, _gridWidth];
        
        for (int i = 0; i < _gridArray.GetLength(0); i++)
        {
            for (int j = 0; j < _gridArray.GetLength(1); j++)
            {
                Debug.DrawLine(new Vector3(j * _cellSizeInUnityUnits, 0, i * _cellSizeInUnityUnits), new Vector3(j * _cellSizeInUnityUnits, 0, (i + 1) * _cellSizeInUnityUnits), Color.red, 100f);
                Debug.DrawLine(new Vector3(j * _cellSizeInUnityUnits, 0, (i + 1) * _cellSizeInUnityUnits), new Vector3((j + 1) * _cellSizeInUnityUnits, 0, (i + 1) * _cellSizeInUnityUnits), Color.red, 100f);
                Debug.DrawLine(new Vector3((j + 1) * _cellSizeInUnityUnits, 0, (i + 1) * _cellSizeInUnityUnits), new Vector3((j + 1) * _cellSizeInUnityUnits, 0, i * _cellSizeInUnityUnits), Color.red, 100f);
                Debug.DrawLine(new Vector3((j + 1) * _cellSizeInUnityUnits, 0, i * _cellSizeInUnityUnits), new Vector3(j * _cellSizeInUnityUnits, 0, i * _cellSizeInUnityUnits), Color.red, 100f);
            }
        }
    }
}
