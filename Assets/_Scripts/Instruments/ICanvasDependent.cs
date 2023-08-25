using UnityEngine;

namespace _Scripts.Instruments
{
    public interface ICanvasDependent
    {
        public void SetCanvas(Canvas canvas);
        public Canvas GetCanvas();
    }
}