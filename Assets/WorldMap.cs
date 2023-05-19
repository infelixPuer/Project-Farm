using System;
using UnityEngine;

public class WorldMap : MonoBehaviour
{
    [SerializeField] 
    private int _worldWidth;

    [SerializeField] 
    private int _worldHeight;

    [SerializeField] 
    private int _cellSizeInUnityUnit;
    
    private void Awake()
    {
        var grid = new Grid(_worldWidth, _worldHeight, _cellSizeInUnityUnit);
    }
}
