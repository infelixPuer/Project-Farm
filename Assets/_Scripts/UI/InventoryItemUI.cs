using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace _Scripts.UI
{
    public class InventoryItemUI : MonoBehaviour
    {
        [FormerlySerializedAs("_sprite")] [SerializeField] 
        private Image _image;

        [SerializeField] 
        private TextMeshProUGUI _tmp;

        public Image GetItemSprite() => _image;
        public TextMeshProUGUI GetTextMeshPro() => _tmp;
        
        public InventoryItemUI Init(Sprite sprite, string text)
        {
            _image.sprite = sprite;
            _tmp.text = text;

            return this;
        }
    }
}