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

        public void AddItem(Item item)
        {
            while (CanAddItem(item))
            {
                var availableStackIndecies = _inventory.GetAllAvailableStackIndecies(item);
                var availableSlotsIndecies = _inventory.GetAllAvailableSlotsIndecies();
                
                if (availableStackIndecies.Length != 0)
                {
                    foreach (var index in availableStackIndecies)
                    {
                        var availableStackSpace = 10 - _inventory[index].Count;
                        
                        _inventory[index].Count += availableStackSpace < item.Count ? availableStackSpace : item.Count;
                        item.Count -= availableStackSpace;
                        
                        if (item.Count <= 0)
                            return;
                    }
                }
                else
                {
                    foreach (var index in availableSlotsIndecies)
                    {
                        _inventory[index].Count += 10 < item.Count ? 10 : item.Count;
                        _inventory[index].ItemData = item.ItemData;
                        item.Count -= 10;
                        
                        if (item.Count <= 0)
                            return;
                    }
                }
            }
        }

        public bool CanAddItem(Item item)
        {
            var availableRoom = 0;
            
            if (_inventory.AvaliableSlot() == -1)
            {
                var stackIndecies = _inventory.GetAllStackIndecies(item);

                if (stackIndecies.Length == 0)
                    return false;

                for (int i = 0; i < stackIndecies.Length; i++)
                {
                    availableRoom += 10 - _inventory[stackIndecies[i]].Count;
                }
                
                return availableRoom >= item.Count;
            }
            else if (_inventory.AvaliableSlot() != -1)
            {
                var slotIndecies = _inventory.GetAllAvailableSlotsIndecies();
                
                for (int i = 0; i < slotIndecies.Length; i++)
                {
                    availableRoom += 10;
                }
                
                return availableRoom >= item.Count;
            }

            return false;

        }

        public bool CheckIfItemCanBeAdded(Item item)
        {
            return (_inventory.ItemCount() == InventorySize) && (_inventory.AvaliableSlot() == -1) && (_inventory.AvaliableStack(item) == -1) ? false : true;
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