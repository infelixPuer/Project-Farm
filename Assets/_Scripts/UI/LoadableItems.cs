using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace _Scripts.UI
{
    public abstract class LoadableItems : MonoBehaviour
    {
        public abstract void LoadItems(ItemUI itemPrefab, GameObject itemContainer, UnityAction action);
    }
}