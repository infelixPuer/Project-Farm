using UnityEngine;

namespace _Scripts.Player.Inventory
{
    [CreateAssetMenu(fileName = "New Item", menuName = "SOs/Inventory/Create new item")]
    public class ItemSO : ScriptableObject
    {
        public string Id;
        public string Name;
        public Sprite Sprite;
        public GameObject Object;
        public ItemTypeSO ItemType;
    }
}