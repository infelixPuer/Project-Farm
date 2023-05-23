public enum GridObjectState
{
    Empty,
    Planted,
    Watered,
    ReadyToHarvest
}

public class GridObject
{
    public GridPosition GridPosition => _gridPosition;

    public GridObjectState ObjectState
    {
        get => _objectState;
        set => _objectState = value;
    }
    
    private Grid _grid;
    private GridPosition _gridPosition;
    private GridObjectState _objectState;

    public GridObject(Grid grid, GridPosition gridPosition, GridObjectState objectState)
    {
        _grid = grid;
        _gridPosition = gridPosition;
        _objectState = objectState;
    }

    private string GetObjectStateName()
    {
        return _objectState switch
        {
            GridObjectState.Empty => "Empty",
            GridObjectState.Planted => "Planted",
            GridObjectState.Watered => "Watered",
            GridObjectState.ReadyToHarvest => "ReadyToHarvest",
            _ => "null"
        };
    }

    public override string ToString()
    {
        return $"x: {_gridPosition.X}; z: {_gridPosition.Z}" +
               $"\nState: {GetObjectStateName()}";
    }
}
