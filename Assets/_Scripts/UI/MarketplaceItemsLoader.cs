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
        private GameObject _itemPrefab;
        
        [SerializeField]
        private MarketplaceBase _marketplaceBase;

        private void Awake()
        {
            _canvas = GetComponentInParent<Canvas>();
            var buttonExit = _canvas.GetComponentInChildren<Button>();
            buttonExit.onClick.AddListener(() => UIManager.Instance.HideCanvas(_canvas));

            foreach (var item in _marketplaceBase.Items)
            {
                var itemObject = Instantiate(_itemPrefab, _itemContainer.transform);
                var button = itemObject.GetComponentInChildren<Button>();
                var tmp = button.gameObject.GetComponentInChildren<TMP_Text>();
                
                if (tmp == null)
                {
                    Debug.LogError("Tmp is null");
                }
                else
                {
                    tmp.text = item.Name;
                }
            }
            
            _canvas.gameObject.SetActive(false);
        }
        
        public void Close()
        {
            _canvas.gameObject.SetActive(false);
            InteractionManager.Instance.IsSelectingSeed = false;
        }
    }
}