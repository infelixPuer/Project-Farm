using UnityEngine;

namespace _Scripts.UI
{
    public abstract class LoadableObject : MonoBehaviour
    {
        public abstract void LoadItems(ItemUI itemPrefab, GameObject itemContainer);
    }
}