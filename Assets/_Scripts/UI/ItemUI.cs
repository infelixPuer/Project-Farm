using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace _Scripts.UI
{
    public class ItemUI : MonoBehaviour
    {
        [FormerlySerializedAs("_sprite")] [SerializeField] 
        private Image _image;

        [SerializeField] 
        private TextMeshProUGUI _tmp;

        public Image GetItemSprite() => _image;
        public TextMeshProUGUI GetTextMeshPro() => _tmp;
        
        public ItemUI Init(Sprite sprite, string text)
        {
            _image.sprite = sprite;
            _tmp.text = text;

            return this;
        }
    }
}