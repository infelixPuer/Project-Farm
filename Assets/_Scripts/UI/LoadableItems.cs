using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace _Scripts.UI
{
    public abstract class LoadableItems : MonoBehaviour
    {
        
        public abstract List<Button> LoadItems(ItemUI itemPrefab, GameObject itemContainer, Action<ItemUI> action);
    }
}