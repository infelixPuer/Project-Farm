﻿using UnityEngine;

namespace _Scripts.Player.Inventory
{
    public class Inventory
    {
        private int _inventorySize = 7;
        private Item[] _inventory;

        public Inventory()
        {
            _inventory = new Item[_inventorySize];
        }

        public bool AddItem(Item item)
        {
            for (int i = 0; i < item.Count; i++)
            {
                if (_inventory.ItemCount() == _inventorySize && (_inventory.AvaliableSlot() != -1))
                    return false;

                if (!_inventory.ContainsItem(item))
                {
                    _inventory.AddItem(item);
                }
                else
                {
                    var availableStackIndex = _inventory.AvaliableStack(item);

                    if (availableStackIndex == -1)
                    {
                        var availableSlotIndex = _inventory.AvaliableSlot();
                    
                        if (availableSlotIndex == -1)
                            return false;
                    
                        _inventory[availableSlotIndex] = new Item(item.ItemData, 1);
                        
                        continue;
                    }
                
                    _inventory[availableStackIndex].Count++;
                }
            }

            return true;
        }

        public void RemoveItem(Item item)
        {
            // TODO: Implement RemoveItem method

            if (!_inventory.ContainsItem(item))
            {
                Debug.LogError("Item not found in inventory");
                return;
            }
            
            var itemIndex = _inventory.ItemIndex(item);
            _inventory[itemIndex].Count -= item.Count;
            
            if (_inventory[itemIndex].Count <= 0)
                _inventory[itemIndex] = new Item();
        }

        public int GetItemCount(Item item)
        {
            return -1;
        }
        
        public Item[] GetItems() => _inventory;
    }
}