using UnityEngine;

namespace _Scripts.Player.Inventory
{
    public class Inventory
    {
        private int _inventorySize = 7;

        public int InventorySize
        {
            get => _inventorySize; 
            set => _inventorySize = value;
        }
        
        private Item[] _inventory;

        public Inventory()
        {
            _inventory = new Item[_inventorySize];
        }

        public Inventory(int size)
        {
            _inventorySize = size;
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
                    if (item.Count <= 10)
                    {
                        _inventory.AddItem(item);
                        return true;
                    }
                    else
                    {
                        _inventory.AddItem(new Item(item.ItemData, 1));
                    }
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
            if (!_inventory.ContainsItem(item))
            {
                Debug.LogError("Item not found in inventory");
                return;
            }
            
            var itemIndex = _inventory.ItemIndex(item);
            
            if (item.Count <= _inventory[itemIndex].Count)
            {
                _inventory[itemIndex].Count -= item.Count;
                _inventory[itemIndex] = _inventory[itemIndex].Count <= 0 ? new Item() : _inventory[itemIndex];
            }
            else
            {
                var indecies = _inventory.GetAllStackIndecies(item);
                
                for (int i = 0; i < indecies.Length; i++)
                {
                    if (item.Count <= _inventory[indecies[i]].Count)
                    {
                        _inventory[indecies[i]].Count -= item.Count;
                        _inventory[indecies[i]] = _inventory[indecies[i]].Count <= 0 ? new Item() : _inventory[indecies[i]];
                        return;
                    }
                    else
                    {
                        item.Count -= _inventory[indecies[i]].Count;
                        _inventory[indecies[i]] = new Item();
                    }
                }   
            }
        }

        public int GetItemCount(Item item)
        {
            var indecies = _inventory.GetAllStackIndecies(item);
            var count = 0;
            
            for (int i = 0; i < indecies.Length; i++)
                count += _inventory[indecies[i]].Count;
            
            return count;
        }
        
        public Item[] GetItems() => _inventory;
    }
}