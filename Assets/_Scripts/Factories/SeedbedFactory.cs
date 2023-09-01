using System;
using _Scripts.World;
using UnityEngine;
using Object = UnityEngine.Object;

namespace _Scripts.Factories
{
    [Serializable]
    public class SeedbedFactory : TileFactory
    {
        [SerializeField]
        private Seedbed _seedbedPrefab;
        
        
        private static SeedbedFactory _instance;
        
        public static SeedbedFactory Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new SeedbedFactory();
                
                return _instance;
            }
        }

        private SeedbedFactory() { }
        
        public override Tile CreateTile()
        {
            var seedbed = Object.Instantiate(_seedbedPrefab, Vector3.zero, Quaternion.identity);
            return seedbed;
        }
    }
}