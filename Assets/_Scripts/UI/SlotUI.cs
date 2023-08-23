using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.UI
{
    public class SlotUI : MonoBehaviour
    {
        [SerializeField]
        private Image _image;
        
        public void SetIcon(Sprite sprite)
        {
            _image.sprite = sprite;
        }
    }
}