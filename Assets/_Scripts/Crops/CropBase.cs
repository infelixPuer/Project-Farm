using _Scripts.Player.Inventory;
using _Scripts.World;
using UnityEngine;

namespace _Scripts.Crops
{
    public abstract class CropBase : MonoBehaviour
    {
        [Range(0f, 1f)]
        [SerializeField] 
        protected float _cropQuality;
        
        public ItemSO Item;
        public int GrowthTime;
        public int Output;
        public float MinimalWaterLevel;

        protected MeshFilter _filter;
        protected MeshRenderer _renderer;
        protected Seedbed _parentSeedbed;

        protected virtual void Init()
        {
            _cropQuality = 1f;
            Item = InteractionManager.Instance.SelectedSeed;
            _filter = GetComponent<MeshFilter>();
            _renderer = GetComponent<MeshRenderer>();
            gameObject.name = Item.Name;
        }

        protected virtual void Plant()
        {
            Item.Object.TryGetComponent<Seed>(out var seed);
            _filter.sharedMesh = seed.CropBase.GetComponent<MeshFilter>().sharedMesh;
            _renderer.sharedMaterial = seed.CropBase.GetComponent<MeshRenderer>().sharedMaterial;
            transform.localScale = seed.CropBase.transform.localScale;
        }

        public virtual void SetParentSeedbed(Seedbed seedbed) => _parentSeedbed = seedbed;
        
        public virtual Seedbed GetParentSeedbed() => _parentSeedbed;
        
        public void SetCropQuality(float value) => _cropQuality = value;
        
        public float GetCropQuality() => _cropQuality;
    }
}