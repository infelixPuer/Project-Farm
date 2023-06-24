using System;
using UnityEngine;

namespace _Scripts.UI
{
    public class MarketplaceUI : MonoBehaviour
    {
        [SerializeField]
        private MarketplaceItemsLoader _inventoryUIContainer;
        
        [SerializeField]
        private MarketplaceItemsLoader _marketplaceUIContainer;

        private void Awake()
        {
            _inventoryUIContainer.ParentMarketplaceUI = this;
            _marketplaceUIContainer.ParentMarketplaceUI = this;
        }

        public void RefreshUI()
        {
            _inventoryUIContainer.Refresh();
            _marketplaceUIContainer.Refresh();
        }
    }
}