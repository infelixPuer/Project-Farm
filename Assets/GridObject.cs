public class GridObject
{
    public GridPosition GridPosition => _gridPosition;
    
    private Grid _grid;
    private GridPosition _gridPosition;

    public GridObject(Grid grid, GridPosition gridPosition)
    {
        _grid = grid;
        _gridPosition = gridPosition;
    }
}
