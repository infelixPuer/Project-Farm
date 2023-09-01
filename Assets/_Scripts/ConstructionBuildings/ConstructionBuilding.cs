using _Scripts.World;
using UnityEngine;

namespace _Scripts.ConstructionBuildings
{
    public class ConstructionBuilding : Tile
    {
        [Header("Size")]
        public int CellSize;
        public int Width;
        public int Depth;

        [Header("Object settings")]
        [SerializeField]
        private Collider _coll;
        
        [SerializeField]
        private Material _material;
        
        public void ToogleColliderState(bool isTrigger) => _coll.isTrigger = isTrigger;

        public void SetObjectTransparent()
        {
            var color = _material.color;
            _material.color = new Color(color.r, color.g, color.b, 0.8f);
        }
        
        public void SetObjectOpaque()
        {
            var color = _material.color;
            _material.color = new Color(color.r, color.g, color.b, 1f);
        }

        public void SetObjectScale(ConstructionBuildingSO buildingData)
        {
            transform.localScale = new Vector3(buildingData.Width * CellSize, 1, buildingData.Depth * CellSize);
        }

        public override void UpdateTileState(TileState state)
        {
            State = state;
        }
    }

}