using System;

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
        
        public bool CanAddToStack() => Count < StackSize;

        public static bool operator ==(Item item1, Item item2) => item1.ItemData == item2.ItemData;

        public static bool operator !=(Item item1, Item item2) => !(item1 == item2);

        public bool Equals(Item other) => Equals(ItemData, other.ItemData) && Count == other.Count;

        public override bool Equals(object obj) => obj is Item other && Equals(other);

        public override int GetHashCode() => HashCode.Combine(ItemData, Count);
    }
}