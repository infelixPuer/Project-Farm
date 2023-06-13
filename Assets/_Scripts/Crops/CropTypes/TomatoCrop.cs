namespace _Scripts.Crops.CropTypes
{
    public class TomatoCrop : CropBase
    {
        private void Awake()
        {
            Init();
            Plant();
            MinimalWaterLevel = 0.5f;
        }
    }
}