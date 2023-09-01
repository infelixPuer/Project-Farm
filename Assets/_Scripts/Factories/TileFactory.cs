using _Scripts.World;
using UnityEngine;

namespace _Scripts.Factories
{
    public abstract class TileFactory
    {
        public abstract Tile CreateTile();
    }
}