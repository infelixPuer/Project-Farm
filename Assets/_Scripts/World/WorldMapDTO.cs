using System;
using System.Collections.Generic;

namespace _Scripts.World
{
    [Serializable]
    public class WorldMapDTO
    {
        public GridDTO Grid { get; set; }
    }
    
    [Serializable]
    public class GridDTO
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public List<GridObjectDTO> GridObjects { get; set; }
    }

    [Serializable]
    public class GridObjectDTO
    {
        public GridPositionDTO GridPosition { get; set; }
    }

    [Serializable]
    public class GridPositionDTO
    {
        public int X { get; set; }
        public int Y { get; set; }
    }
}