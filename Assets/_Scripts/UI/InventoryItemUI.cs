using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.UI
{
    public class InventoryItemUI : MonoBehaviour
    {
        [SerializeField] 
        private Image _sprite;

        [SerializeField] 
        private TextMeshProUGUI _tmp;

        public Image GetItemSprite() => _sprite;
        public TextMeshProUGUI GetTextMeshPro() => _tmp;
    }
}