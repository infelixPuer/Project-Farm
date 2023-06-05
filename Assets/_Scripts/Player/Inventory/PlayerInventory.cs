﻿using System.Collections.Generic;
using UnityEngine;

namespace _Scripts.Player.Inventory
{
    public class PlayerInventory : MonoBehaviour
    {
        public Inventory Inventory;
        public ItemSO Tomato;
        public ItemSO Corn;
        public ItemSO Onion;
        public ItemSO Carrot;

        private bool _itemAddedSuccsessfuly;

        private void Awake()
        {
            this.Inventory = new Inventory();
            AddItem(Tomato);
            AddItem(Tomato);
            AddItem(Tomato);
            AddItem(Tomato);
            AddItem(Tomato);
            AddItem(Tomato);
            AddItem(Tomato);
            AddItem(Tomato);
            AddItem(Tomato);
            AddItem(Corn);
            AddItem(Corn);
            AddItem(Corn);
            AddItem(Corn);
            AddItem(Corn);
            AddItem(Corn);
            AddItem(Corn);
            AddItem(Carrot);
            AddItem(Carrot);
            AddItem(Carrot);
            AddItem(Carrot);
            AddItem(Carrot);
            AddItem(Carrot);
            AddItem(Carrot);
            AddItem(Carrot);
            AddItem(Carrot);
            AddItem(Carrot);
            AddItem(Carrot);
            AddItem(Carrot);
            AddItem(Onion);
            AddItem(Onion);
            AddItem(Onion);
        }

        public void AddItem(ItemSO item)
        {
            _itemAddedSuccsessfuly = this.Inventory.AddItem(new Item(item, 1));
            
            if (_itemAddedSuccsessfuly)
                Debug.Log("Item added!");
            else 
                Debug.LogWarning("Inventory is full!");
        }

        public Item[] GetInventory() => Inventory.GetItems();
    }
}