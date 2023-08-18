using System;
using System.Collections.Generic;
using _Scripts.UI;
using TMPro;
using UnityEngine;

namespace _Scripts.Player.Inventory
{
    public class PlayerInventory : LoadableItems, IInventorable
    {
        [SerializeField] 
        private TextMeshProUGUI _currentBalanceText;

        public static PlayerInventory Instance;
        
        public Wallet Wallet;
        public Inventory Inventory;
        public ItemSO Tomato;
        public ItemSO Corn;
        public ItemSO Onion;
        public ItemSO Carrot;

        private void Awake()
        {
            if (Instance is null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
            
            Inventory = new Inventory();
            Wallet = new Wallet();
            Wallet.BalanceChanged += UpdateBalanceText;
            
            AddToBalance(300);

            #region Populating inventory with items

            // AddItem(Tomato, 9); 
            // AddItem(Corn, 7);
            // AddItem(Carrot, 12);
            // AddItem(Onion, 3);

            #endregion
        }
        
        private void UpdateBalanceText(object sender, int value) => _currentBalanceText.text = "Balance: " + value.ToString();

        public void AddItem(ItemSO item, int count) => Inventory.AddItem(new Item(item, count));

        public void RemoveItem(ItemSO item, int count) => Inventory.RemoveItem(new Item(item, count));

        public bool CheckIfItemCanBeAdded(Item item) => Inventory.CanAddItem(item);

        public void AddToBalance(int value) => Wallet.AddMoney(value);

        public void RemoveFromBalance(int value) => Wallet.RemoveMoney(value);

        public Item[] GetInventoryItems() => Inventory.GetItems();

        public override List<ItemUI> LoadItems(ItemUI itemPrefab, GameObject itemContainer, Action<ItemUI> action)
        {
            var inventory = Inventory.GetItems();
            var itemObjects = new List<ItemUI>();
            
            for (int i = 0; i < inventory.Length; i++)
            {
                if (inventory[i].IsEmpty)
                {
                    Instantiate(itemPrefab.Init(null, null, null,null), itemContainer.transform);
                    continue;
                }
            
                var itemObject = Instantiate(itemPrefab, itemContainer.transform);
                itemObject.Init(inventory[i].ItemData.Sprite, inventory[i].Count, inventory[i].ItemData.Price, inventory[i].ItemData);
                itemObject.SetButtonAction(() => action(itemObject));
                itemObjects.Add(itemObject);
            }
            
            return itemObjects;
        }

        public int GetItemCount(Item item) => Inventory.GetItemCount(item);

        public bool CheckRequiredItems(Item[] requiredItems) => Inventory.CheckRequiredItems(requiredItems);
    }
}