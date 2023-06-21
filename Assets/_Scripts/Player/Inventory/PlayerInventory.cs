using System.Collections.Generic;
using _Scripts.UI;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

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

        public void AddItem(ItemSO item, int count)
        {
            _itemAddedSuccsessfuly = Inventory.AddItem(new Item(item, count));
            
            // if (_itemAddedSuccsessfuly)
            //     Debug.Log("Item added!");
            // else 
            //     Debug.LogWarning("Inventory is full!");
        }

        public void AddToBalance(int value)
        {
            Wallet.AddMoney(value);
            _currentBalanceText.text = "Balance: " + Wallet.Balance.ToString();
        }
        
        public void RemoveFromBalance(int value)
        {
            Wallet.RemoveMoney(value);
            _currentBalanceText.text = "Balance: " + Wallet.Balance.ToString();
        }

        public Item[] GetInventoryItems() => Inventory.GetItems();

        public override void LoadItems(ItemUI itemPrefab, GameObject itemContainer, UnityAction action)
        {
            var inventory = Inventory.GetItems();
            var buttons = new List<Button>();
            
            for (int i = 0; i < inventory.Length; i++)
            {
                if (inventory[i].IsEmpty)
                {
                    Instantiate(itemPrefab.Init(null, "", null), itemContainer.transform);
                    continue;
                }
            
                var itemObject = Instantiate(itemPrefab.Init(inventory[i].ItemData.Sprite, inventory[i].Count.ToString(), action), itemContainer.transform);
                itemObject.SetButtonAction(action);
            }
        }
    }
}