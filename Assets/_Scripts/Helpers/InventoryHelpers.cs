using System.Collections.Generic;

namespace _Scripts.Player.Inventory
{
    public static class InventoryHelpers
    {
        public static int ItemCount(this Item[] inventory)
        {
            int count = 0;

            for (int i = 0; i < inventory.Length; i++)
            {
                count = inventory[i].IsEmpty ? count : count + 1;
            }

            return count;
        }

        public static bool ContainsItem(this Item[] inventory, Item item)
        {
            for (int i = 0; i < inventory.Length; i++)
                if (inventory[i] == item)
                    return true;

            return false;
        }

        public static void AddItem(this Item[] inventory, Item item)
        {
            for (int i = 0; i < inventory.GetLength(0); i++)
                if (inventory[i].IsEmpty)
                {
                    inventory[i] = item;
                    return;
                }
        }

        public static int ItemIndex(this Item[] inventory, Item item)
        {
            for (int i = 0; i < inventory.Length; i++)
                if (inventory[i] == item)
                    return i;
            
            return -1;
        }

        public static int AvaliableStack(this Item[] inventory, Item item)
        {
            for (int i = 0; i < inventory.Length; i++)
            {
                if (inventory[i] == item && inventory[i].CanAddToStack())
                    return i;
            }
            
            return -1;
        }

        public static int AvaliableSlot(this Item[] inventory)
        {
            for (int i = 0; i < inventory.Length; i++)
                if (inventory[i].IsEmpty)
                    return i;

            return -1;
        }
        
        public static int[] GetAllStackIndecies(this Item[] inventory, Item item)
        {
            var indecies = new List<int>();
            
            for (int i = 0; i < inventory.Length; i++)
            {
                if (inventory[i] == item)
                    indecies.Add(i);
            }

            return indecies.ToArray();
        }
        
        public static int[] GetAllAvailableStackIndecies(this Item[] inventory, Item item)
        {
            var indecies = new List<int>();
            
            for (int i = 0; i < inventory.Length; i++)
            {
                if (inventory[i] == item && inventory[i].CanAddToStack())
                    indecies.Add(i);
            }

            return indecies.ToArray();
        }

        public static int[] GetAllAvailableSlotsIndecies(this Item[] inventory)
        {
            var indecies = new List<int>();
            
            for (int i = 0; i < inventory.Length; i++)
            {
                if (inventory[i].IsEmpty)
                    indecies.Add(i);
            }
            
            return indecies.ToArray();
        }
    }
}