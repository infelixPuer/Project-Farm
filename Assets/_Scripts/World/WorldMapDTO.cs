using System;

namespace _Scripts.World
{
    [Serializable]
    public class WorldMapDTO
    {
        public GridDTO Grid;
    }
    
    [Serializable]
    public class GridDTO
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public GridObjectDTO[,] GridObject;

        public GridDTO()
        {
            GridObject = new GridObjectDTO[Width, Height];
        }
    }

    [Serializable]
    public class GridObjectDTO
    {
        public GridPositionDTO GridPosition { get; set; }
    }

    [Serializable]
    public struct GridPositionDTO
    {
        public int X { get; set; }
        public int Y { get; set; }
    }
}