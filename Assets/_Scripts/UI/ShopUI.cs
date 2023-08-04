using _Scripts.Player.Inventory;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.UI
{
    public class ShopUI : MonoBehaviour
    {
        [SerializeField]
        private ShopItemsLoader _inventoryUIContainer;
        
        [SerializeField]
        private ShopItemsLoader shopUIContainer;

        [SerializeField] 
        private Button _exitButton;

        [SerializeField]
        private Canvas _canvas;
        
        [SerializeField]
        private TextMeshProUGUI _currentBalanceText;

        public Canvas ShopCanvas => _canvas;

        private void Awake()
        {
            SetParentForContainers();
            ConfigureExitButton();
            SetBalanceText();
        }
        
        public ShopItemsLoader GetInventoryUIContainer() => _inventoryUIContainer;
        
        public ShopItemsLoader GetShopUIContainer() => shopUIContainer;

        private void SetParentForContainers()
        {
            _inventoryUIContainer.parentShopUI = this;
            shopUIContainer.parentShopUI = this;
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
            shopUIContainer.Refresh();
            SetBalanceText();
        }
    }
}