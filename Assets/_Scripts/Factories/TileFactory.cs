using _Scripts.World;
using UnityEngine;

namespace _Scripts.Factories
{
    public abstract class TileFactory : MonoBehaviour
    {
        public abstract Tile CreateTile(Transform parent, Vector3 position);
        public abstract Tile RecreateTile(Transform parent, Vector3 position, TileDTO tile);
    }
}