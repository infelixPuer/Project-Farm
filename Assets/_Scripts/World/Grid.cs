using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Scripts.World
{
    public class Grid
    {
        public GridObject[,] GridObjects => _gridObjectArray;
        
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
            new((int)Mathf.Floor(position.x / _cellSizeInUnityUnits),
                (int)Mathf.Floor(position.z / _cellSizeInUnityUnits));

        public List<GridPosition> GetGridPositions(Vector3 position, Vector3 scale)
        {
            var width = (int)Mathf.Floor(scale.x / _cellSizeInUnityUnits);
            var depth = (int)Mathf.Floor(scale.z / _cellSizeInUnityUnits);
            var gridPositions = new List<GridPosition>();

            for (int x = 0; x < width; x++)
            {
                for (int z = 0; z < depth; z++)
                {
                    gridPositions.Add(GetGridPosition(position + new Vector3(x, 0, z) * _cellSizeInUnityUnits));
                }
            }

            return gridPositions;
        }

        public GridObject GetGridObject(GridPosition gridPosition)
        {
            try
            {
                return _gridObjectArray[gridPosition.X, gridPosition.Z];
            }
            catch (IndexOutOfRangeException)
            {
                return null;
            }
        }

        public void CreateGridObjects(Transform prefab)
        {
            foreach (var gridObject in _gridObjectArray)
            {
                var debugTransform = GameObject.Instantiate(prefab, GetWorldPosition(gridObject.GridPosition), Quaternion.identity, WorldMap.Instance.transform);
                debugTransform.GetComponent<GridDebugObject>().GridObject = _gridObjectArray[gridObject.GridPosition.X, gridObject.GridPosition.Z];
            }
        }
    }
}
