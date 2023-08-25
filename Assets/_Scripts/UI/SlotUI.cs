using System;
using System.Linq;
using _Scripts.Instruments;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.UI
{
    public class SlotUI : MonoBehaviour
    {
        [SerializeField]
        private Image _image;
        
        [SerializeField]
        private Image _background;

        [SerializeField]
        private Color _selectedColor;
        
        [SerializeField]
        private Color _unselectedColor;
        
        public InstrumentBase Instrument { get; private set; }
        private InstrumentBase _instrumentObject;

        private Interactor _interactor;

        private void Awake()
        {
            _interactor = FindObjectOfType<Interactor>();
        }

        public void Init(Sprite sprite, InstrumentBase instrument)
        {
            _image.sprite = sprite;
            Instrument = instrument;
            
            if (Instrument is ICanvasDependent canvasDependent)
                canvasDependent.SetCanvas(GetNeededCanvas(canvasDependent));
        }

        private Canvas GetNeededCanvas(ICanvasDependent canvasDependent)
        {
            var canvas = canvasDependent.GetCanvas();
            
            if (canvas is null)
                throw new NullReferenceException("Canvas is null");

            var neededCanvas = InstrumentSlotsManager.Instance.Canvases.Where(x => x.name == canvas.name)
                .Select(x => x)
                .First();

            return neededCanvas;
        }

        public void SelectSlot()
        {
            if (_background.color == _selectedColor)
            {
                DeselectSlot();
                return;
            }
            
            _background.color = _selectedColor;
            
            if (Instrument is not null)
            {
                _instrumentObject = Instantiate(Instrument, Vector3.zero, Quaternion.identity);
                var previousInstrument = _interactor.DropItemFromHand();
                
                if (previousInstrument is not null)
                {
                    Destroy(previousInstrument.gameObject);
                    _interactor.ResetItemInHand();
                }
                
                _interactor.GetItemInHand(_instrumentObject);
            }
        }

        public void DeselectSlot()
        {
            if (_background.color == _selectedColor && Instrument is not null)
            {
                Destroy(_instrumentObject.gameObject);
                _interactor.ResetItemInHand();
                _instrumentObject = null;
            }
            
            _background.color = _unselectedColor;
        }
    }
}