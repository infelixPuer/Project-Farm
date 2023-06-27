using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Scripts.UI
{
    public abstract class LoadableItems : MonoBehaviour
    {
        public abstract List<ItemUI> LoadItems(ItemUI itemPrefab, GameObject itemContainer, Action<ItemUI> action);
    }
}