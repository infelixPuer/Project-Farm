using UnityEngine;

namespace _Scripts.Crops.CropStates
{
    public class CropReadyToHarvestState : CropBaseState
    {
        public override void EnterCropState(CropStateMachine stateMachine)
        {
            Debug.LogWarning($"Crop quality on harvest: {stateMachine.GetCrop().GetCropQuality()}");
            stateMachine.IsReadyToHarvest = true;
        }

        public override void UpdateCropState(CropStateMachine stateMachine) { }
    }
}