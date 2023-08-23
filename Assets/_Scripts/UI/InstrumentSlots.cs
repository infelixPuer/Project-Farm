using System;
using System.Collections.Generic;
using System.Linq;
using _Scripts.Instruments;
using _Scripts.Player.Inventory;
using UnityEngine;

namespace _Scripts.UI
{
    public class InstrumentSlots : MonoBehaviour
    {
        [SerializeField]
        private SlotUI _slotPrefab;
        
        public List<InstrumentBase> _instruments = new List<InstrumentBase>();
        public List<SlotUI> Slots { get; private set; } = new List<SlotUI>();

        private void OnEnable()
        {
            foreach (var instrument in _instruments)
            {
                var slot = Instantiate(_slotPrefab, transform);
                slot.Init(instrument.GetIconSprite(), () => {}, instrument);
                Slots.Add(slot);
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