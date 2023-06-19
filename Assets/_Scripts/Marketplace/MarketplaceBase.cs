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
    
    public abstract class MarketplaceBase : MonoBehaviour, IInteractable
    {
        [SerializeField] 
        private Canvas _shopUI;

        [SerializeField] 
        private PlayerMovement _playerMovement;
        
        public ShopType ShopType;

        public List<ItemSO> Items { get; private set; }

        public virtual void LoadItems()
        {
            Items = Resources.LoadAll<ItemSO>($"Scriptables/InventoryItems/{Enum.GetName(typeof(ShopType), ShopType)}/").ToList();
        }
        
        public void Interact()
        {
            UIManager.Instance.ShowCanvas(_shopUI);
            
            // _shopUI.gameObject.SetActive(true);
            // InteractionManager.Instance.IsSelectingSeed = true;
            // _playerMovement.enabled = false;
            // TimeManager.Instance.TimeBlocked = true;
            // Cursor.visible = true;
            // Cursor.lockState = CursorLockMode.Confined;
        }

        public void Interact(RaycastHit hitInfo) { }
    }
}