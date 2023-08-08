using UnityEngine;

namespace _Scripts.ConstructionBuildings
{
    public class ConstructionBuilding : MonoBehaviour
    {
        [field: SerializeField] 
        public int CellSize { get; set; }

        [field: SerializeField] 
        public int Width { get; set; }

        [field: SerializeField] 
        public int Depth { get; set; }

        [SerializeField]
        private Collider _coll;
        
        [SerializeField]
        private Material _material;

        private void Awake()
        {
            transform.localScale = new Vector3(Width * CellSize, 1, Depth * CellSize);
        }
        
        public void ToogleColliderState(bool isTrigger) => _coll.isTrigger = isTrigger;

        public void SetObjectTransparent()
        {
            var color = _material.color;
            _material.SetFloat("_Mode", 3);
            _material.color = new Color(color.r, color.g, color.b, 0.8f);
        }
        
        public void SetObjectOpaque()
        {
            var color = _material.color;
            _material.SetFloat("_Mode", 0);
            _material.color = new Color(color.r, color.g, color.b, 1f);
        }
    }

}