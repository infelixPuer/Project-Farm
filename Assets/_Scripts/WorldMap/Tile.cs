﻿using UnityEngine;

public abstract class Tile : MonoBehaviour
{
        public TileState State = TileState.Empty;

        public abstract void UpdateTileState(TileState state);
}

public enum TileState
{
        Empty,
        Occupied
}