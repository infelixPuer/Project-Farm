namespace _Scripts.World
{
    public enum GridObjectState
    {
        Empty,
        Occupied
    }

    public class GridObject
    {
        public GridPosition GridPosition
        {
            get => _gridPosition;
            private set => _gridPosition = value;
        }

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

        public GridObject(Grid grid, GridObjectDTO gridObjectDTO)
        {
            _grid = grid;
            GridPosition = new GridPosition(gridObjectDTO.GridPosition.X, gridObjectDTO.GridPosition.Y);
            _gridObjectState = GridObjectState.Empty;
        }

        public override string ToString() =>
            $"{_gridPosition}" +
            $"\nCell state: {State}";
    }
}
