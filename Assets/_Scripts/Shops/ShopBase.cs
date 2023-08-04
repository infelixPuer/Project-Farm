using System;
using System.Collections.Generic;
using System.Linq;
using _Scripts.Player.Interaction;
using _Scripts.Player.Inventory;
using _Scripts.UI;
using UnityEngine;

namespace _Scripts.World
{
    public enum ShopType
    {
        Food,
        Seeds,
        ConstructionMaterials
    }
    
    public abstract class ShopBase : LoadableItems, IInteractable, IInventorable
    {
        [SerializeField] 
        private Canvas _shopUI;
        
        [SerializeField]
        private ShopItemsLoader _shopUIContainer;
        
        public List<ItemSO> Items { get; private set; }
        public ShopType ShopType;
        public bool IsOpened { get; set; }

        private Inventory _shopInventory = new(15);
        
        public virtual void LoadInventory()
        {
            Items = Resources.LoadAll<ItemSO>($"Scriptables/InventoryItems/{Enum.GetName(typeof(ShopType), ShopType)}/").ToList();

            foreach (var item in Items)
                _shopInventory.AddItem(new Item(item, 10));
        }
        
        public void Interact(Interactor interactor)
        {
            if (IsOpened) return;
            
            IsOpened = true;
            _shopUIContainer.ItemStorage = this;
            UIManager.Instance.ShowCanvas(_shopUI);
        }

        public override List<ItemUI> LoadItems(ItemUI itemPrefab, GameObject itemContainer, Action<ItemUI> action)
        {
            var itemObjects = new List<ItemUI>();
            var inventory = _shopInventory.GetItems();

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

        public int GetItemCount(Item item) => _shopInventory.GetItemCount(item);

        public void AddItem(ItemSO item, int amount) => _shopInventory.AddItem(new Item(item, amount));

        public void RemoveItem(ItemSO item, int amount) => _shopInventory.RemoveItem(new Item(item, amount));

        public bool CheckIfItemCanBeAdded(Item item) => _shopInventory.CanAddItem(item);
        
        public void CloseShop() => IsOpened = false;
    }
}