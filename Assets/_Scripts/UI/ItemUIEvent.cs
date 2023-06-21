using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace _Scripts.UI
{
    public class ItemUIEvent : MonoBehaviour
    {
        [SerializeField] 
        private Button _button;

        public UnityAction OnButtonPressed;
        
        private void Awake()
        {
            _button.onClick.AddListener(OnButtonPressed);
        }
    }
}