using _Scripts.Crops;
using _Scripts.Player.Inventory;
using UnityEngine;

namespace _Scripts.Player.Interaction.InteractionTypes
{
    public class HarvestCrop : MonoBehaviour, IInteractable
    {
        [SerializeField] 
        private CropBase _cropBase;
        
        [SerializeField]
        private CropStateMachine _cropStateMachine;
        
        private PlayerInventory _inventory;
        
        public ItemSO Item { set; private get; }

        private void Awake()
        {
            _inventory = FindObjectOfType<PlayerInventory>();
        }

        public void Interact()
        {
            if (!_cropStateMachine.IsReadyToHarvest) return;
            
            _inventory.AddItem(Item, _cropBase.Output);
            _cropBase.GetParentSeedbed().UpdateTileState(TileState.Empty);
            Destroy(_cropBase.gameObject);
        }

        public void Interact(RaycastHit hitInfo)
        {
            
        }
    }
}