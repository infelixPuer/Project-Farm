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
    
    private void Awake()
    {
        var grid = new Grid(_worldWidth, _worldHeight, _cellSizeInUnityUnit);

        for (int i = 0; i < _worldWidth; i++)
        {
            for (int j = 0; j < _worldHeight; j++)
            {
                var cell = Instantiate(_cellPrefab, new Vector3(i + 0.5f, 0, j + 0.5f) * _cellSizeInUnityUnit, Quaternion.identity, transform);
                cell.name = $"Cell: {i} {j}";
            }
        }
    }
}
