public abstract class CropBaseState
{
    public CropScriptableObject Crop;
    public abstract void EnterCropState(CropStateMachine stateMachine);
    public abstract void UpdateCropState(CropStateMachine crop);
}
