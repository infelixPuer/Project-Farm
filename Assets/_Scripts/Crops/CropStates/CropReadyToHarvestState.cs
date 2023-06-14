﻿namespace _Scripts.Crops.CropStates
{
    public class CropReadyToHarvestState : CropBaseState
    {
        public override void EnterCropState(CropStateMachine stateMachine)
        {
            stateMachine.IsReadyToHarvest = true;
        }

        public override void UpdateCropState(CropStateMachine stateMachine)
        {
            
        }
    }
}