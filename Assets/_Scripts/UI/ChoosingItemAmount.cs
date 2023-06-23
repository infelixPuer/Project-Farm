using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace _Scripts.UI
{
    public class ChoosingItemAmount : MonoBehaviour
    {
        [SerializeField] 
        private TextMeshProUGUI _tmp;

        [SerializeField] 
        private Slider _slider;
        
        [SerializeField]
        private TextMeshProUGUI _buttonText;
        
        [SerializeField]
        private Button _button;

        private void Awake()
        {
            _slider.onValueChanged.AddListener((value) => _tmp.text = value.ToString());
        }
        
        public void SetButtonText(string text) => _buttonText.text = text;
        
        public void SetSliderMaxValue(int maxValue) => _slider.maxValue = maxValue;
        
        public void AddListenerToSlider(UnityAction<float> action)
        {
            _slider.onValueChanged.AddListener(action);
        }

        public int GetSliderValue() => (int)_slider.value;

        public void SetButtonAction(UnityAction action)
        {
            _button.onClick.RemoveAllListeners();
            _button.onClick.AddListener(action);
        }
    }
}