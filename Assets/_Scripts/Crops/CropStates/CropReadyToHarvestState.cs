using UnityEngine;

namespace _Scripts.Crops.CropStates
{
    public class CropReadyToHarvestState : CropBaseState
    {
        public override void EnterCropState(CropStateMachine crop)
        {
           crop.GetHarvestCrop().enabled = true;
           // crop.GetHarvestCrop().Item
        }

        public override void UpdateCropState(CropStateMachine crop)
        {
            
        }
    }
}