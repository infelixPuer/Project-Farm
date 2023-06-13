using UnityEngine;

namespace _Scripts.Crops.CropStates
{
    public class CropWiltingState : CropBaseState
    {
        public override void EnterCropState(CropStateMachine stateMachine)
        {
            Debug.Log("Enter Wilting State");
        }

        public override void UpdateCropState(CropStateMachine stateMachine)
        {
            
        }
    }
}