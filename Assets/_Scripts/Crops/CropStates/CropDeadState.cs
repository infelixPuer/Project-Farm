using UnityEngine;

namespace _Scripts.Crops.CropStates
{
    public class CropDeadState : CropBaseState
    {
        public override void EnterCropState(CropStateMachine stateMachine)
        {
            Debug.LogWarning("Enter Dead State");
            stateMachine.IsReadyToHarvest = true;
        }

        public override void UpdateCropState(CropStateMachine stateMachine)
        {
            
        }
    }
}