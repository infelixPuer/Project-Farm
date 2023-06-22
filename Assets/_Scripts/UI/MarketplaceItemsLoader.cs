using System;
using System.Collections.Generic;
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

        public MarketplaceInteractionType InteractionType;

        private Canvas _canvas;
        private List<Button> _buttons;
        private ChoosingItemAmount _chooseItemGameObject;
        private Button _selectedButton;
        private Color _selectedColor = Color.red;
        private Color _defaultColor = Color.white;

        private void Awake()
        {
            _canvas = GetComponentInParent<Canvas>();
            var buttonExit = _canvas.GetComponentInChildren<Button>();
            buttonExit.onClick.AddListener(() => UIManager.Instance.HideCanvas(_canvas));
        }

        private void OnEnable()
        {
            var buttons = _itemStorage.LoadItems(_itemPrefab, _itemContainer.gameObject, OnItemPressed);
            buttons.ForEach(x => x.onClick.AddListener(() => OnButtonSelected(x)));
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
            var rectTransform = _chooseItemGameObject.GetComponent<RectTransform>();
            rectTransform.anchorMin = _itemContainer.anchorMin;
            rectTransform.anchorMax = _itemContainer.anchorMax;
        }

        private void OnButtonSelected(Button button)
        {
            if (_selectedButton is not null)
            {
                _selectedButton.image.color = _defaultColor;
            }
            
            _selectedButton = button;
            _selectedButton.image.color = _selectedColor;
        }
    }
}