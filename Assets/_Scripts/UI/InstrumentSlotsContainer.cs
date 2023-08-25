using _Scripts.Instruments;
using UnityEngine;

namespace _Scripts.UI
{
    public class InstrumentSlotsContainer : MonoBehaviour
    {
        public void SelectSlot(int index)
        {
            InstrumentSlotsManager.Instance.Slots.ForEach(slot =>
            {
                if (slot != InstrumentSlotsManager.Instance.Slots[index])
                    slot.DeselectSlot();
            });
            
            InstrumentSlotsManager.Instance.Slots[index].SelectSlot();
        }
    }
}