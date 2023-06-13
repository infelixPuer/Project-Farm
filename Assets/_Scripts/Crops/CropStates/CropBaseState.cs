using _Scripts.Player.Inventory;

public abstract class CropBaseState
{
    public ItemSO Crop;
    public abstract void EnterCropState(CropStateMachine stateMachine);
    public abstract void UpdateCropState(CropStateMachine stateMachine);
}
