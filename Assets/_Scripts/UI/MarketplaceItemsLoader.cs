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
        private GameObject _itemContainer;
        
        [SerializeField]
        private ItemUI _itemPrefab;

        [SerializeField] 
        private LoadableItems _itemStorage;

        public MarketplaceInteractionType InteractionType;

        private Canvas _canvas;
        private List<Button> _buttons;

        private void Awake()
        {
            _canvas = GetComponentInParent<Canvas>();
            var buttonExit = _canvas.GetComponentInChildren<Button>();
            buttonExit.onClick.AddListener(() => UIManager.Instance.HideCanvas(_canvas));
        }

        private void Start()
        {
            _buttons = _itemStorage.LoadItems(_itemPrefab, _itemContainer, null);
        }

        public void Close()
        {
            _canvas.gameObject.SetActive(false);
            InteractionManager.Instance.IsSelectingSeed = false;
        }
        
        
    }
}