using UnityEngine;

namespace _Scripts.World
{
    public enum TileState
    {
        Empty,
        Occupied
    }
    
    public abstract class Tile : MonoBehaviour
    {
        public TileState State = TileState.Empty;

        public abstract void UpdateTileState(TileState state);
    }
}