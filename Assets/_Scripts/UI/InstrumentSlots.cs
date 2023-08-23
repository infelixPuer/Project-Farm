using System;
using System.Collections.Generic;
using _Scripts.Instruments;
using UnityEngine;

namespace _Scripts.UI
{
    public class InstrumentSlots : MonoBehaviour
    {
        [SerializeField, Range(1, 10)]
        private int _slotsCount = 7;
        
        [SerializeField]
        private SlotUI _slotPrefab;
        
        public List<InstrumentBase> _instruments = new List<InstrumentBase>();
        public List<SlotUI> Slots { get; private set; } = new List<SlotUI>();

        private void OnEnable()
        {
            for (var i = 0; i < _slotsCount; i++)
            {
                try
                {
                    var instrument = _instruments[i];
                    var slot = Instantiate(_slotPrefab, transform);
                    slot.Init(instrument.GetIconSprite(), instrument);
                    Slots.Add(slot);
                }
                catch (ArgumentOutOfRangeException)
                {
                    var slot = Instantiate(_slotPrefab, transform);
                    slot.Init(null, null);
                    Slots.Add(slot);
                }
            }
        }

        public void SelectSlot(int index)
        {
            Slots.ForEach(slot =>
            {
                if (slot != Slots[index])
                    slot.DeselectSlot();
            });
            
            Slots[index].SelectSlot();
        }
    }
}