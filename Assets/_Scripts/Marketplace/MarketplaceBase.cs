using System;
using System.Collections.Generic;
using System.Linq;
using _Scripts.Player.Interaction;
using _Scripts.Player.Inventory;
using _Scripts.UI;
using UnityEngine;
using UnityEngine.Events;

namespace _Scripts.World
{
    public enum ShopType
    {
        Food,
        Seeds
    }
    
    public abstract class MarketplaceBase : LoadableItems, IInteractable
    {
        [SerializeField] 
        private Canvas _shopUI;
        
        public ShopType ShopType;

        public List<ItemSO> Items { get; private set; }

        public virtual void LoadItems()
        {
            Items = Resources.LoadAll<ItemSO>($"Scriptables/InventoryItems/{Enum.GetName(typeof(ShopType), ShopType)}/").ToList();
        }
        
        public void Interact()
        {
            UIManager.Instance.ShowCanvas(_shopUI);
        }

        public void Interact(RaycastHit hitInfo) { }

        public override void LoadItems(ItemUI itemPrefab, GameObject itemContainer, UnityAction action)
        {
            foreach (var item in Items)
            {
                var itemObject = Instantiate(itemPrefab.Init(item.Sprite, $"{item.Price}", action), itemContainer.transform);
                itemObject.SetButtonAction(action);
            }
        }
    }
}