using System;
using _Scripts.Instruments;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace _Scripts.UI
{
    public class SlotUI : MonoBehaviour
    {
        [SerializeField]
        private Image _image;

        [SerializeField]
        private Button _button;
        
        [SerializeField]
        private Image _background;

        [SerializeField]
        private Color _selectedColor;
        
        [SerializeField]
        private Color _unselectedColor;
        
        public InstrumentBase Instrument { get; private set; }

        private Interactor _interactor;

        private void Awake()
        {
            _interactor = FindObjectOfType<Interactor>();
        }

        public void Init(Sprite sprite, UnityAction buttonAction, InstrumentBase instrument)
        {
            _image.sprite = sprite;
            _button.onClick.AddListener(buttonAction);
            Instrument = instrument;
        }

        public void SelectSlot()
        {
            if (_background.color == _selectedColor)
            {
                DeselectSlot();
                return;
            }
            
            _background.color = _selectedColor;
            _interactor.GetItemInHand(Instrument);
        }

        public void DeselectSlot()
        {
            if (_background.color == _selectedColor)
                _interactor.DropItemFromHand();
            
            _background.color = _unselectedColor;
        }
    }
}