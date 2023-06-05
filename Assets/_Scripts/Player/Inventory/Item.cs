using System;
using System.Data.SqlTypes;

namespace _Scripts.Player.Inventory
{
    public struct Item
    {
        public ItemSO ItemData;
        public int Count;
        public const int StackSize = 10;
        public bool IsEmpty => ItemData == null;
        
        public Item(ItemSO itemData, int count)
        {
            ItemData = itemData;
            Count = count;
        }
        
        public bool IsFullStack() => Count >= StackSize;

        public static bool operator ==(Item item1, Item item2)
        {
            return item1.ItemData == item2.ItemData;
        }

        public static bool operator !=(Item item1, Item item2)
        {
            return !(item1 == item2);
        }
        public bool Equals(Item other)
        {
            return Equals(ItemData, other.ItemData) && Count == other.Count;
        }

        public override bool Equals(object obj)
        {
            return obj is Item other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(ItemData, Count);
        }
    }
}