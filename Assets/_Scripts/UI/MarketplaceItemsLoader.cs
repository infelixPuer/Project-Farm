using System.Collections.Generic;
using _Scripts.Player.Inventory;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum MarketplaceInteractionType
{
    Buy,
    Sell
}

// TODO: Refactor this class

namespace _Scripts.UI
{
    public class MarketplaceItemsLoader : MonoBehaviour
    {
        [SerializeField]
        private RectTransform _itemContainer;
        
        [SerializeField]
        private ItemUI _itemPrefab;

        [SerializeField] 
        private LoadableItems _itemStorage;
        
        [SerializeField]
        private TextMeshProUGUI _currentBalanceText;

        [SerializeField] 
        private ChoosingItemAmount _chooseAmountOfItemsPrefab;

        [SerializeField]
        private Color _selectedColor = Color.red;
        
        [SerializeField]
        private Color _defaultColor = Color.white;

        public MarketplaceInteractionType InteractionType;

        private MarketplaceUI _parentMarketplaceUI;
        
        public MarketplaceUI ParentMarketplaceUI { private get; set; }
        
        private Canvas _canvas;
        private List<Button> _buttons;
        private ChoosingItemAmount _chooseItemGameObject;
        private Button _selectedButton;

        private void Awake()
        {
            _canvas = GetComponentInParent<Canvas>();
            var buttonExit = _canvas.GetComponentInChildren<Button>();
            buttonExit.onClick.AddListener(() => UIManager.Instance.HideCanvas(_canvas));
        }

        private void OnEnable()
        {
            var itemObjects = _itemStorage.LoadItems(_itemPrefab, _itemContainer.gameObject, OnItemPressed);
            itemObjects.ForEach(x => x.SetButtonAction(() => OnItemSelected(x)));

            if (_currentBalanceText is not null)
            {
                _currentBalanceText.text = "Money: " + _itemStorage.GetComponent<PlayerInventory>()?.Wallet.Balance.ToString();
            }
        }

        private void OnDisable()
        {
            foreach (Transform child in transform)
                Destroy(child.gameObject);
        }

        public void Close()
        {
            _canvas.gameObject.SetActive(false);
            InteractionManager.Instance.IsSelectingSeed = false;
        }
        
        private void OnItemPressed(ItemUI item)
        {
            if (_chooseItemGameObject is not null)
            {
                Destroy(_chooseItemGameObject.gameObject);
            }
            
            _chooseItemGameObject = Instantiate(_chooseAmountOfItemsPrefab, _canvas.transform);
            _chooseItemGameObject.SetButtonText(InteractionType == MarketplaceInteractionType.Buy ? "Buy" : "Sell");
            _chooseItemGameObject.SetSliderMaxValue(item.Count ?? 10);
            _chooseItemGameObject.AddListenerToSlider((value) => _chooseItemGameObject.SetButtonAction(() => OnConfrimationButtonClick(item, (int)value)));
            
            var rectTransform = _chooseItemGameObject.GetComponent<RectTransform>();
            rectTransform.anchorMin = _itemContainer.anchorMin;
            rectTransform.anchorMax = _itemContainer.anchorMax;
        }

        private void OnItemSelected(ItemUI item)
        {
            if (_selectedButton is not null)
            {
                _selectedButton.image.color = _defaultColor;
            }
            
            _selectedButton = item.GetButton();
            _selectedButton.image.color = _selectedColor;
            
            var value = _chooseItemGameObject.GetSliderValue();
            _chooseItemGameObject.SetButtonAction(() => OnConfrimationButtonClick(item, value));
        }

        private void OnConfrimationButtonClick(ItemUI item, int amount)
        {
            var playerInventory = PlayerInventory.Instance;
            var price = item.ItemData.Price;
            var totalPrice = price * amount;
            
            if (InteractionType == MarketplaceInteractionType.Buy)
            {
                if (playerInventory.Wallet.Balance < totalPrice)
                {
                    Debug.Log("Not enough money");
                    return;
                }
                
                playerInventory.Wallet.RemoveMoney(totalPrice);
                playerInventory.AddItem(item.ItemData, amount);
            }
            else
            {
                if (item.Count < amount)
                {
                    Debug.Log("Not enough items");
                    return;
                }
                
                playerInventory.Wallet.AddMoney(totalPrice);
                playerInventory.RemoveItem(item.ItemData, amount);
            }
            
            if (ParentMarketplaceUI is not null)
                ParentMarketplaceUI.RefreshUI();
        }

        public void Refresh()
        {
            foreach (Transform child in transform)
                Destroy(child.gameObject);

            if (_chooseItemGameObject is not null)
            {
                Destroy(_chooseItemGameObject.gameObject);
                _chooseItemGameObject = null;
                _selectedButton = null;
            }
            
            var itemObjects = _itemStorage.LoadItems(_itemPrefab, _itemContainer.gameObject, OnItemPressed);
            itemObjects.ForEach(x => x.SetButtonAction(() => OnItemSelected(x)));

            if (_currentBalanceText is not null)
            {
                _currentBalanceText.text = "Money: " + PlayerInventory.Instance.Wallet.Balance.ToString();
            }
        }
    }
}