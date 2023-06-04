using UnityEngine;

namespace _Scripts.Player.Inventory
{
    [CreateAssetMenu(fileName = "New ItemType", menuName = "SOs/Inventory/Create new item type")]
    public class ItemTypeSO : ScriptableObject
    {
        public bool IsStackable;
        public ItemCategory Category;

        public override string ToString()
        {
            return Category.ToString();
        }
    }

    public enum ItemCategory
    {
        Instrument,
        Food,
        Seed,
        ConstructionMaterial
    }
}