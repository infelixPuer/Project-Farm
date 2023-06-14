using UnityEngine;

namespace _Scripts.Crops.CropStates
{
    public class CropDeadState : CropBaseState
    {
        public override void EnterCropState(CropStateMachine stateMachine)
        {
            Debug.Log("Enter Dead State");
        }

        public override void UpdateCropState(CropStateMachine stateMachine)
        {
            
        }
    }
}