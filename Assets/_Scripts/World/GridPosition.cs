namespace _Scripts.World
{
    public struct GridPosition
    {
        public int X => _x;
        public int Z => _z;

        private int _x;
        private int _z;

        public GridPosition(int x, int z)
        {
            _x = x;
            _z = z;
        }

        public override string ToString() => $"x: {_x}; z: {_z}";
    }
}
