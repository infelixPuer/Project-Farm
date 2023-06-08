namespace _Scripts.Crops.CropStates
{
    public class CropReadyToHarvestState : CropBaseState
    {
        public override void EnterCropState(CropStateMachine crop)
        {
            crop.IsReadyToHarvest = true;
        }

        public override void UpdateCropState(CropStateMachine crop)
        {
            
        }
    }
}