using System;
using _Scripts.Player.Inventory;
using _Scripts.World;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.UI
{
    public class MarketplaceItemsLoader : MonoBehaviour
    {
        private Canvas _canvas;
        
        [SerializeField]
        private GameObject _itemContainer;
        
        [SerializeField]
        private ItemUI _itemPrefab;

        [SerializeField] 
        private LoadableObject _itemStorage;

        private void Awake()
        {
            _canvas = GetComponentInParent<Canvas>();
            var buttonExit = _canvas.GetComponentInChildren<Button>();
            buttonExit.onClick.AddListener(() => UIManager.Instance.HideCanvas(_canvas));
        }

        private void Start()
        {
            _itemStorage.LoadItems(_itemPrefab, _itemContainer);
        }

        public void Close()
        {
            _canvas.gameObject.SetActive(false);
            InteractionManager.Instance.IsSelectingSeed = false;
        }
    }
}