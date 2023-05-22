public struct GridPosition
{
    private int _x;
    private int _z;
    
    public GridPosition(int x, int z)
    {
        _x = x;
        _z = z;
    }

    public override string ToString()
    {
        return $"x: {_x}; z: {_z}";
    }
}
