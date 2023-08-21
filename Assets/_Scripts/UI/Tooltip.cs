using _Scripts.Player.Inventory;
using TMPro;
using UnityEngine;

namespace _Scripts.UI
{
    public class Tooltip : MonoBehaviour
    {
        [SerializeField]
        private TextMeshPro _text;
        
        public void SetText(string text)
        {
            _text.text = text;
        }
        
        public void Show()
        {
            gameObject.SetActive(true);
        }
        
        public void Hide()
        {
            gameObject.SetActive(false);
        }

        private void Update()
        {
            if (!gameObject.activeSelf)
                return;

            var direction = (transform.position - PlayerInventory.Instance.transform.position).normalized;
            direction.y = 0;
            
            transform.rotation = Quaternion.LookRotation(direction);
        }
    }
}