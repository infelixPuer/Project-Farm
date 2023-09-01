using System;
using System.Collections.Generic;

namespace _Scripts.World
{
    [Serializable]
    public class WorldMapDTO
    {
        public GridDTO Grid;
    }

    [Serializable]
    public class SeedbedDTO : TileDTO
    {
        public float DaysToDry;
        public float CurrentWaterLevel;
        public float WaterLevelAfterWatering;
        public TimeSpan ElapsedTime;
        public DateTime DateOfWatering;
        public bool IsWatered;
    }

    public abstract class TileDTO
    {
        public bool IsExisting;
    }
    
    [Serializable]
    public class GridDTO
    {
        public int Width;
        public int Height;
        public List<GridObjectDTO> GridObjects;
    }

    [Serializable]
    public class GridObjectDTO
    {
        public GridPositionDTO GridPosition;
        public TileDTO Child = new SeedbedDTO();
    }

    [Serializable]
    public class GridPositionDTO
    {
        public int X;
        public int Y;
    }
}