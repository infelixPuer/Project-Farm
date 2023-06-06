using System;
using _Scripts.Player.Inventory;
using UnityEngine;

namespace _Scripts.Player.Interaction.InteractionTypes
{
    public class HarvestCrop : MonoBehaviour, IInteractable
    {
        private PlayerInventory _inventory;
        
        public ItemSO Item { set; private get; }

        private void Awake()
        {
            _inventory = FindObjectOfType<PlayerInventory>();
        }

        public void Interact()
        {
            _inventory.AddItem(Item);
        }

        public void Interact(RaycastHit hitInfo)
        {
            
        }
    }
}