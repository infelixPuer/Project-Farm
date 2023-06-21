﻿using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace _Scripts.UI
{
    public class ItemUI : MonoBehaviour
    {
        [Header("Visuals")]
        [SerializeField] 
        private Image _image;

        [SerializeField] 
        private TextMeshProUGUI _tmp;
        
        [Header("Functionality")]
        [SerializeField]
        private Button _button;

        public Image GetItemSprite() => _image;
        public TextMeshProUGUI GetTextMeshPro() => _tmp;
        
        public ItemUI Init(Sprite sprite, string text, UnityAction action)
        {
            _image.sprite = sprite;
            _tmp.text = text;
            
            return this;
        }
        
        public void SetButtonAction(UnityAction action)
        {
            _button.onClick.RemoveAllListeners();
            _button.onClick.AddListener(action);
        }
    }
}