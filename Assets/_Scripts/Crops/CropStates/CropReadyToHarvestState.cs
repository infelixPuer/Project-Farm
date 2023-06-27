using UnityEngine;

namespace _Scripts.Crops.CropStates
{
    public class CropReadyToHarvestState : CropBaseState
    {
        public override void EnterCropState(CropStateMachine stateMachine)
        {
            Debug.LogWarning($"Crop scale on harvest: {stateMachine.transform.localScale.x}");
            stateMachine.IsReadyToHarvest = true;
        }

        public override void UpdateCropState(CropStateMachine stateMachine) { }
    }
}