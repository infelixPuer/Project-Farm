using System;

namespace _Scripts.Crops.CropTypes
{
    public class OnionCrop : CropBase
    {
        private void Awake()
        {
            Init();
            Plant();
        }
    }
}