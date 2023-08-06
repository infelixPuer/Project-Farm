using UnityEngine;

namespace _Scripts.Instruments
{
    public class WoodenHammer : InstrumentBase
    {
        public override void MainAction()
        {
            Debug.Log("Building!");
        }

        public override void SecondaryAction()
        {
            Debug.Log("Choosing what to build!");
        }
    }
}