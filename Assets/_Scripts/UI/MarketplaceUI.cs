using System.Collections.Generic;
using _Scripts.Player.Inventory;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.UI
{
    public class MarketplaceUI : MonoBehaviour
    {
        [SerializeField]
        private MarketplaceItemsLoader _inventoryUIContainer;
        
        [SerializeField]
        private MarketplaceItemsLoader _marketplaceUIContainer;

        [SerializeField] 
        private Button _exitButton;

        [SerializeField]
        private Canvas _canvas;
        
        [SerializeField]
        private TextMeshProUGUI _currentBalanceText;

        public Canvas MarketplaceCanvas => _canvas;

        private void Awake()
        {
            SetParentForContainers();
            ConfigureExitButton();
            SetBalanceText();
        }
        
        public MarketplaceItemsLoader GetInventoryUIContainer() => _inventoryUIContainer;
        
        public MarketplaceItemsLoader GetMarketplaceUIContainer() => _marketplaceUIContainer;

        private void SetParentForContainers()
        {
            _inventoryUIContainer.ParentMarketplaceUI = this;
            _marketplaceUIContainer.ParentMarketplaceUI = this;
        }
        
        private void ConfigureExitButton() => _exitButton.onClick.AddListener(() => UIManager.Instance.HideCanvas(_canvas));

        private void SetBalanceText()
        {
            if (_currentBalanceText is not null)
                _currentBalanceText.text = "Money: " + PlayerInventory.Instance.Wallet.Balance.ToString();
        }

        public void RefreshUI()
        {
            _inventoryUIContainer.Refresh();
            _marketplaceUIContainer.Refresh();
            SetBalanceText();
        }
    }
}