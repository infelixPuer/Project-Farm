public abstract class CropBaseState
{
    public Crop Crop;
    public abstract void EnterCropState(CropStateMachine stateMachine);
    public abstract void UpdateCropState(CropStateMachine stateMachine);
}
