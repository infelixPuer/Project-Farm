using System;
using System.Collections.Generic;
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

        private void OnEnable()
        {
            foreach (var instrument in _instruments)
            {
                var slot = Instantiate(_slotPrefab, transform);
                slot.SetIcon(instrument.GetIconSprite());
            }
        }
    }
}