using System;
using System.Collections.Generic;
using System.Linq;
using _Scripts.Player.Inventory;
using _Scripts.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CropsLoaderUI : MonoBehaviour
{
    [SerializeField] 
    private GameObject _panel;

    [SerializeField] 
    private ItemUI _panelItemPrefab;

    private List<Item> _seeds = new();

    private void Awake()
    {
        // _seeds = Resources.LoadAll<ItemSO>("Scriptables/InventoryItems/Seeds/").ToList();
        //
        // foreach (var seed in _seeds)
        // {
        //     var item = Instantiate(_panelItemPrefab, _panel.transform);
        //     var button = item.GetComponentInChildren<Button>();
        //     var tmp = button.gameObject.GetComponentInChildren<TMP_Text>();
        //
        //     if (tmp == null)
        //     {
        //         Debug.LogError("Tmp is null");
        //     }
        //     else
        //     {
        //         tmp.text = seed.Name;
        //     }
        //     
        //     button.onClick.AddListener(() => OnSeedSelected(seed));
        // }
    }

    private void OnEnable()
    {
        _seeds = PlayerInventory.Instance.Inventory.GetItems()
            .Select(x => x)
            .Where(x => !x.IsEmpty && x.ItemData.ItemType.Category == ItemCategory.Seed)
            .ToList();
        
        foreach (var seed in _seeds)
        {
            var item = Instantiate(_panelItemPrefab, _panel.transform);
            item.Init(seed.ItemData.Sprite, seed.Count, seed.ItemData);
            item.SetButtonAction(() => OnSeedSelected(seed.ItemData));
        }
    }
    
    private void OnDisable()
    {
        foreach (Transform child in _panel.transform)
        {
            Destroy(child.gameObject);
        }
    }

    private void OnSeedSelected(ItemSO seed) => InteractionManager.Instance.SelectedSeed = seed;
}
