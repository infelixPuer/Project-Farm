using TMPro;
using UnityEngine;
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

        private void Update()
        {
            _tmp.text = _slider.value.ToString();
        }
        
        public void SetButtonText(string text) => _buttonText.text = text;
        
        public void SetSliderMaxValue(int maxValue) => _slider.maxValue = maxValue;
    }
}