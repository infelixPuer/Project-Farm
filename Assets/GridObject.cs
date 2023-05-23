

public class GridObject
{
    public GridPosition GridPosition => _gridPosition;

    public Cell Cell
    {
        get => _cell;
        set => _cell = value;
    }
    
    private Grid _grid;
    private GridPosition _gridPosition;
    private Cell _cell;

    public GridObject(Grid grid, GridPosition gridPosition)
    {
        _grid = grid;
        _gridPosition = gridPosition;
    }

    public override string ToString()
    {
        return $"{_gridPosition}" +
               $"\nCell state: {Cell.State}";
    }
}
