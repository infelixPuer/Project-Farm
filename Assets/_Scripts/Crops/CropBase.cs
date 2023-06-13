using _Scripts.Player.Inventory;
using UnityEngine;

namespace _Scripts.Crops
{
    public abstract class CropBase : MonoBehaviour
    {
        public ItemSO Item;
        public int GrowthTime;
        public int Output;
        public float MinimalWaterLevel;

        protected MeshFilter _filter;
        protected MeshRenderer _renderer;
        protected Seedbed _parentSeedbed;

        protected virtual void Init()
        {
            _filter = GetComponent<MeshFilter>();
            _renderer = GetComponent<MeshRenderer>();
            Item = InteractionManager.Instance.SelectedCrop;
            gameObject.name = Item.Name;
        }

        protected virtual void Plant()
        {
            _filter.sharedMesh = Item.Object.GetComponent<MeshFilter>().sharedMesh;
            _renderer.sharedMaterial = Item.Object.GetComponent<MeshRenderer>().sharedMaterial;
            transform.localScale = Item.Object.transform.localScale;
        }

        public virtual void SetParentSeedbed(Seedbed seedbed) => _parentSeedbed = seedbed;
        public virtual Seedbed GetParentSeedbed() => _parentSeedbed;
    }
}