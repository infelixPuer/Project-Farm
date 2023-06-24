using System;
using System.Collections.Generic;
using System.Linq;
using _Scripts.Player.Interaction;
using _Scripts.Player.Inventory;
using _Scripts.UI;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Scripts.World
{
    public enum ShopType
    {
        Food,
        Seeds
    }
    
    public abstract class MarketplaceBase : LoadableItems, IInteractable
    {
        [FormerlySerializedAs("_shopUI")] [SerializeField] 
        private Canvas _marketplaceUI;
        
        public ShopType ShopType;

        public List<ItemSO> Items { get; private set; }

        // TODO: Change name of one of the LoadItems methods
        
        public virtual void LoadItems()
        {
            Items = Resources.LoadAll<ItemSO>($"Scriptables/InventoryItems/{Enum.GetName(typeof(ShopType), ShopType)}/").ToList();
        }
        
        public void Interact()
        {
            UIManager.Instance.ShowCanvas(_marketplaceUI);
        }

        public void Interact(RaycastHit hitInfo) { }

        public override List<ItemUI> LoadItems(ItemUI itemPrefab, GameObject itemContainer, Action<ItemUI> action)
        {
            var itemObjects = new List<ItemUI>();
            
            foreach (var item in Items)
            {
                var itemObject = Instantiate(itemPrefab, itemContainer.transform);
                itemObject.Init(item.Sprite, item.Price, item);
                itemObject.SetButtonAction(() => action(itemObject));
                itemObjects.Add(itemObject);
            }

            return itemObjects;
        }
    }
}