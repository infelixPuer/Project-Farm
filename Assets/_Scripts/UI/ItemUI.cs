using System;
using _Scripts.Player.Inventory;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace _Scripts.UI
{
    public class ItemUI : MonoBehaviour
    {
        [Header("Visuals")]
        [SerializeField] 
        private Image _image;

        [SerializeField] 
        private TextMeshProUGUI _itemCount;
        
        [Header("Functionality")]
        [SerializeField]
        private Button _button;

        public ItemSO ItemData { get; private set; }
        public int? Count { get; private set; }

        public Image GetItemSprite() => _image;
        public TextMeshProUGUI GetTextMeshPro() => _itemCount;

        public ItemUI Init(Sprite sprite, int? count, ItemSO item)
        {
            _image.sprite = sprite;
            _itemCount.text = count.ToString();
            Count = count;
            ItemData = item;
            
            return this;
        }
        
        public void SetButtonAction(UnityAction action)
        {
            _button.onClick.AddListener(action);
        }

        public void SubtractFromCount(int amount)
        {
            if (amount > Count)
                Debug.LogError("Amount is bigger than count");
            
            Count -= amount;
            
            if (Count == 0)
                Destroy(gameObject);
        }
        
        public Button GetButton() => _button;
    }
}