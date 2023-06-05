using UnityEngine;

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
                if (inventory[i] == item && !inventory[i].IsFullStack())
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
    }
}