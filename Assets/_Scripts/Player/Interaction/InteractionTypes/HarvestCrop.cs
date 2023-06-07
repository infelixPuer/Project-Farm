using _Scripts.Player.Inventory;
using UnityEngine;

namespace _Scripts.Player.Interaction.InteractionTypes
{
    public class HarvestCrop : MonoBehaviour, IInteractable
    {
        [SerializeField] 
        private Crop _crop;
        
        private PlayerInventory _inventory;
        
        public ItemSO Item { set; private get; }

        private void Awake()
        {
            _inventory = FindObjectOfType<PlayerInventory>();
        }

        public void Interact()
        {
            _inventory.AddItem(Item);
            _crop.GetParentSeedbed().UpdateTileState(TileState.Empty);
            Destroy(_crop.gameObject);
        }

        public void Interact(RaycastHit hitInfo)
        {
            
        }
    }
}