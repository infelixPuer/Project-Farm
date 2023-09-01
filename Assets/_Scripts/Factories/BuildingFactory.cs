using _Scripts.World;

namespace _Scripts.Factories
{
    public class BuildingFactory : TileFactory
    {
        private static BuildingFactory _instance;

        public static BuildingFactory Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new BuildingFactory();

                return _instance;
            }
        }

        private BuildingFactory() { }
        
        public override Tile CreateTile()
        {
            throw new System.NotImplementedException();
        }
    }
}