using _Scripts.Player.Inventory;
using UnityEngine;
using UnityEngine.UI;

public enum MarketplaceInteractionType
{
    Buy,
    Sell
}

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
        private ChoosingItemAmount _chooseAmountOfItemsPrefab;

        [SerializeField]
        private Color _selectedColor = Color.red;
        
        [SerializeField]
        private Color _defaultColor = Color.white;

        public MarketplaceInteractionType InteractionType;
        
        public MarketplaceUI ParentMarketplaceUI { private get; set; }
        
        private ChoosingItemAmount _chooseItemGameObject;
        private Button _selectedButton;

        private void OnEnable()
        {
            var itemObjects = _itemStorage.LoadItems(_itemPrefab, _itemContainer.gameObject, OnItemPressed);
            itemObjects.ForEach(x => x.SetButtonAction(() => OnItemSelected(x)));
        }

        private void OnDisable()
        {
            foreach (Transform child in transform)
                Destroy(child.gameObject);
        }
        
        private void OnItemPressed(ItemUI item)
        {
            if (_chooseItemGameObject is not null)
            {
                Destroy(_chooseItemGameObject.gameObject);
            }
            
            _chooseItemGameObject = Instantiate(_chooseAmountOfItemsPrefab, ParentMarketplaceUI.MarketplaceCanvas.transform);
            _chooseItemGameObject.SetButtonText(InteractionType == MarketplaceInteractionType.Buy ? "Buy" : "Sell");
            _chooseItemGameObject.SetSliderMaxValue(PlayerInventory.Instance.Inventory.GetItemCount(new Item(item.ItemData, item.Count ?? 0)) ?? 10);
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
        }
    }
}