using System.Collections.Generic;
using UnityEngine;

namespace _Scripts.Player.Inventory
{
    public class Inventory
    {
        private int _inventorySize = 7;
        private Dictionary<ItemSO, int> _inventory;

        public Inventory()
        {
            _inventory = new Dictionary<ItemSO, int>(_inventorySize);
        }

        public bool AddItem(ItemSO item)
        {
            if (_inventory.Count == _inventorySize) 
                return false;

            if (!_inventory.ContainsKey(item))
                _inventory.Add(item, 1);
            else
                _inventory[item]++;

            return true;
        }

        public Dictionary<ItemSO, int> GetItems() => _inventory;
    }
}