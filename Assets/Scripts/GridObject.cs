

public class GridObject
{
    public GridPosition GridPosition => _gridPosition;

    public Seedbed seedbed
    {
        get => _seedbed;
        set => _seedbed = value;
    }
    
    private Grid _grid;
    private GridPosition _gridPosition;
    private Seedbed _seedbed;

    public GridObject(Grid grid, GridPosition gridPosition)
    {
        _grid = grid;
        _gridPosition = gridPosition;
    }

    public override string ToString()
    {
        return $"{_gridPosition}" +
               $"\nCell state: {seedbed?.State ?? SeedbedState.Empty}";
    }
}
