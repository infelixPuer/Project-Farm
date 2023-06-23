using System;
using System.Collections.Generic;
using _Scripts.UI;
using TMPro;
using UnityEngine;

namespace _Scripts.Player.Inventory
{
    public class PlayerInventory : LoadableItems
    {
        [SerializeField] 
        private TextMeshProUGUI _currentBalanceText;
        
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
            Wallet.BalanceChanged += UpdateBalanceText;
            
            AddToBalance(100);
            RemoveFromBalance(50);
            RemoveFromBalance(150);
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
        
        private void UpdateBalanceText(object sender, int value)
        {
            _currentBalanceText.text = "Balance: " + value.ToString();
        }

        public void AddItem(ItemSO item, int count)
        {
            _itemAddedSuccsessfuly = Inventory.AddItem(new Item(item, count));
            
            // if (_itemAddedSuccsessfuly)
            //     Debug.Log("Item added!");
            // else 
            //     Debug.LogWarning("Inventory is full!");
        }

        public void RemoveItem(ItemSO item, int count)
        {
            Inventory.RemoveItem(new Item(item, count));
        }

        public void AddToBalance(int value)
        {
            Wallet.AddMoney(value);
        }
        
        public void RemoveFromBalance(int value)
        {
            Wallet.RemoveMoney(value);
        }

        public Item[] GetInventoryItems() => Inventory.GetItems();

        public override List<ItemUI> LoadItems(ItemUI itemPrefab, GameObject itemContainer, Action<ItemUI> action)
        {
            var inventory = Inventory.GetItems();
            var itemObjects = new List<ItemUI>();
            
            for (int i = 0; i < inventory.Length; i++)
            {
                if (inventory[i].IsEmpty)
                {
                    Instantiate(itemPrefab.Init(null, null, null), itemContainer.transform);
                    continue;
                }
            
                var itemObject = Instantiate(itemPrefab, itemContainer.transform);
                itemObject.Init(inventory[i].ItemData.Sprite, inventory[i].Count, inventory[i].ItemData);
                itemObject.SetButtonAction(() => action(itemObject));
                itemObjects.Add(itemObject);
            }
            
            return itemObjects;
        }
    }
}