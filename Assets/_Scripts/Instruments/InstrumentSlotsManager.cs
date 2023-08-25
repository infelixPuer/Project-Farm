using System;
using System.Collections.Generic;
using _Scripts.UI;
using UnityEngine;

namespace _Scripts.Instruments
{
    public class InstrumentSlotsManager : MonoBehaviour
    {
        [Header("Slots")]
        [SerializeField]
        private RectTransform _instrumentSlotsContainer;

        [SerializeField]
        private SlotUI _slotPrefab;
        
        [SerializeField, Range(1, 10)]
        private int _numberOfSlots = 7;

        [Header("UI dependencies")]
        public List<Canvas> Canvases = new List<Canvas>();
        
        public List<InstrumentBase> Instruments = new List<InstrumentBase>();
        public List<SlotUI> Slots { get; private set; } = new List<SlotUI>();
        
        public static InstrumentSlotsManager Instance;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
            
            SetupSlots();
        }

        private void SetupSlots()
        {
            for (var i = 0; i < _numberOfSlots; i++)
            {
                try
                {
                    var instrument = Instruments[i];
                    var slot = Instantiate(_slotPrefab, _instrumentSlotsContainer.transform);
                    slot.Init(instrument.GetIconSprite(), instrument);
                    Slots.Add(slot);
                }
                catch (ArgumentOutOfRangeException)
                {
                    var slot = Instantiate(_slotPrefab, _instrumentSlotsContainer.transform);
                    slot.Init(null, null);
                    Slots.Add(slot);
                }
            }
        }
    }
}