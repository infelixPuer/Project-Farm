namespace _Scripts.Crops.CropTypes
{
    public class CarrotCrop : CropBase
    {
        private void Awake()
        {
            Init();
            Plant();
            MinimalWaterLevel = 0.4f;
        }
    }
}