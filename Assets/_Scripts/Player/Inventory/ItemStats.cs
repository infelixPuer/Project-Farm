using UnityEngine;

namespace _Scripts.Player.Inventory
{
    [CreateAssetMenu(fileName = "New item stats", menuName = "SOs/Inventory/Create new item stats")]
    class ItemStats : ScriptableObject
    {
        public float Durability;
        public float HealthEffect;
        public float StaminaEffect;
    }
}