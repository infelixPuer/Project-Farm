using _Scripts.Crops;
using _Scripts.Player.Inventory;
using UnityEngine;

namespace _Scripts.Player.Interaction.InteractionTypes
{
    public class HarvestCrop : MonoBehaviour, IInteractable
    {
        [SerializeField] 
        private Crop _crop;

        [SerializeField] 
        private CropBase _cropBase;
        
        private PlayerInventory _inventory;
        
        public ItemSO Item { set; private get; }

        private void Awake()
        {
            _inventory = FindObjectOfType<PlayerInventory>();
        }

        public void Interact()
        {
            _inventory.AddItem(Item, _cropBase.Output);
            _cropBase.GetParentSeedbed().UpdateTileState(TileState.Empty);
            Destroy(_cropBase.gameObject);
        }

        public void Interact(RaycastHit hitInfo)
        {
            
        }
    }
}