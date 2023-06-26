namespace _Scripts.Player.Inventory
{
    public interface IInventorable
    {
        public int GetItemCount(Item item);
        public void AddItem(ItemSO item, int amount);
        public void RemoveItem(ItemSO item, int amount);
    }
}