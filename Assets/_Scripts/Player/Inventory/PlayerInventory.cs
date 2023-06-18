using UnityEngine;

namespace _Scripts.Player.Inventory
{
    public class PlayerInventory : MonoBehaviour
    {
        public Wallet Wallet;
        public Inventory Inventory;
        public ItemSO Tomato;
        public ItemSO Corn;
        public ItemSO Onion;
        public ItemSO Carrot;

        private bool _itemAddedSuccsessfuly;

        private void Awake()
        {
            Inventory = new Inventory();
            Wallet = new Wallet();
            
            Wallet.AddMoney(100);
            Wallet.RemoveMoney(50);
            Wallet.RemoveMoney(150);
            AddItem(Tomato, 1);
            AddItem(Tomato, 1);
            AddItem(Tomato, 1);
            AddItem(Tomato, 1);
            AddItem(Tomato, 1);
            AddItem(Tomato, 1);
            AddItem(Tomato, 1);
            AddItem(Tomato, 1);
            AddItem(Tomato, 1);    
            AddItem(Corn, 1);
            AddItem(Corn, 1);
            AddItem(Corn, 1);
            AddItem(Corn, 1);
            AddItem(Corn, 1);
            AddItem(Corn, 1);
            AddItem(Corn, 1);
            AddItem(Carrot, 1);
            AddItem(Carrot, 1);
            AddItem(Carrot, 1);
            AddItem(Carrot, 1);
            AddItem(Carrot, 1);
            AddItem(Carrot, 1);
            AddItem(Carrot, 1);
            AddItem(Carrot, 1);
            AddItem(Carrot, 1);
            AddItem(Carrot, 1);
            AddItem(Carrot, 1);
            AddItem(Carrot, 1);
            AddItem(Onion, 1);
            AddItem(Onion, 1);
            AddItem(Onion, 1);
        }

        public void AddItem(ItemSO item, int count)
        {
            _itemAddedSuccsessfuly = Inventory.AddItem(new Item(item, count));
            
            // if (_itemAddedSuccsessfuly)
            //     Debug.Log("Item added!");
            // else 
            //     Debug.LogWarning("Inventory is full!");
        }

        public Item[] GetInventoryItems() => Inventory.GetItems();
    }
}