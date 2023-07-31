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
        Seeds
    }
    
    public abstract class MarketplaceBase : LoadableItems, IInteractable, IInventorable
    {
        [SerializeField] 
        private Canvas _marketplaceUI;
        
        [SerializeField]
        private MarketplaceItemsLoader _marketplaceUIContainer;
        
        public List<ItemSO> Items { get; private set; }
        public ShopType ShopType;
        public bool IsOpened { get; set; }

        private Inventory _marketplaceInventory = new(15);
        
        public virtual void LoadInventory()
        {
            Items = Resources.LoadAll<ItemSO>($"Scriptables/InventoryItems/{Enum.GetName(typeof(ShopType), ShopType)}/").ToList();

            foreach (var item in Items)
                _marketplaceInventory.AddItem(new Item(item, 10));
        }
        
        public void Interact(Interactor interactor)
        {
            if (IsOpened) return;
            
            IsOpened = true;
            _marketplaceUIContainer.ItemStorage = this;
            UIManager.Instance.ShowCanvas(_marketplaceUI);
        }

        public override List<ItemUI> LoadItems(ItemUI itemPrefab, GameObject itemContainer, Action<ItemUI> action)
        {
            var itemObjects = new List<ItemUI>();
            var inventory = _marketplaceInventory.GetItems();

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

        public int GetItemCount(Item item) => _marketplaceInventory.GetItemCount(item);

        public void AddItem(ItemSO item, int amount) => _marketplaceInventory.AddItem(new Item(item, amount));

        public void RemoveItem(ItemSO item, int amount) => _marketplaceInventory.RemoveItem(new Item(item, amount));

        public bool CheckIfItemCanBeAdded(Item item) => _marketplaceInventory.CanAddItem(item);
        
        public void CloseMarketplace() => IsOpened = false;
    }
}