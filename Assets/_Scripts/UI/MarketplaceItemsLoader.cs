using System;
using System.Collections.Generic;
using _Scripts.World;
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

        private void Awake()
        {
            _canvas = GetComponentInParent<Canvas>();
            var buttonExit = _canvas.GetComponentInChildren<Button>();
            buttonExit.onClick.AddListener(() => UIManager.Instance.HideCanvas(_canvas));
        }

        private void Start()
        {
            _itemStorage.LoadItems(_itemPrefab, _itemContainer.gameObject, OnItemPressed);
            
            // foreach (var button in _buttons)
            // {
            //     button.onClick.AddListener(OnItemPressed);
            // }
        }

        public void Close()
        {
            _canvas.gameObject.SetActive(false);
            InteractionManager.Instance.IsSelectingSeed = false;
        }
        
        public void OnItemPressed()
        {
            if (_chooseItemGameObject is not null)
            {
                Destroy(_chooseItemGameObject.gameObject);
            }
            
            _chooseItemGameObject = Instantiate(_chooseAmountOfItemsPrefab, _canvas.transform);
            _chooseItemGameObject.SetButtonText(InteractionType == MarketplaceInteractionType.Buy ? "Buy" : "Sell");
            var rectTransform = _chooseItemGameObject.GetComponent<RectTransform>();
            rectTransform.anchorMin = _itemContainer.anchorMin;
            rectTransform.anchorMax = _itemContainer.anchorMax;
        }
    }
}