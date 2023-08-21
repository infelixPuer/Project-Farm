namespace _Scripts.World
{
    public enum GridObjectState
    {
        Empty,
        Occupied
    }

    public class GridObject
    {
        public GridPosition GridPosition => _gridPosition;

        public GridObjectState State
        {
            get => _gridObjectState;
            set => _gridObjectState = value;
        }

        public Tile Tile
        {
            get => _tile;
            set => _tile = value;
        }

        private Grid _grid;
        private GridPosition _gridPosition;
        private GridObjectState _gridObjectState;
        private Tile _tile;

        public GridObject(Grid grid, GridPosition gridPosition)
        {
            _grid = grid;
            _gridPosition = gridPosition;
            _gridObjectState = GridObjectState.Empty;
        }

        public override string ToString() =>
            $"{_gridPosition}" +
            $"\nCell state: {State}";
    }
}
