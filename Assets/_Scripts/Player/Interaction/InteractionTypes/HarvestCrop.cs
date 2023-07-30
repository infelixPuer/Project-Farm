using System;
using _Scripts.Crops;
using _Scripts.Player.Inventory;
using _Scripts.Helpers;
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

        public void Interact(Interactor interactor)
        {
            if (!_cropStateMachine.IsReadyToHarvest) return;
            
            var initialItemCount = Mathf.RoundToInt(_cropBase.GetCropQuality() * _cropBase.Output);
            var itemCount = GaussianRandomNumberGenerator.GenerateRandomNumber(initialItemCount, 1f);

            if (!_inventory.Inventory.CanAddItem(new Item(Item, (int)Math.Round(itemCount))))
            {
                Debug.Log("Inventory is full");
                return;
            }
            
            _inventory.AddItem(Item, (int)Math.Round(itemCount));
            _cropBase.GetParentSeedbed().UpdateTileState(TileState.Empty);
            Destroy(_cropBase.gameObject);
        }
    }
}