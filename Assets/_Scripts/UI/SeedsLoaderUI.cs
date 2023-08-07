using System;
using System.Collections.Generic;
using System.Linq;
using _Scripts.Player.Inventory;
using _Scripts.UI;
using UnityEngine;

public class SeedsLoaderUI : MonoBehaviour
{
    [SerializeField] 
    private GameObject _contentPanel;

    [SerializeField] 
    private ItemUI _UIItemPrefab;

    public event EventHandler<Sprite> SeedSelected;

    private List<Item> _seeds = new();

    private void OnEnable()
    {
        _seeds = PlayerInventory.Instance.Inventory.GetItems()
            .Select(x => x)
            .Where(x => !x.IsEmpty && x.ItemData.ItemType.Category == ItemCategory.Seed)
            .ToList();
        
        foreach (var seed in _seeds)
        {
            var item = Instantiate(_UIItemPrefab, _contentPanel.transform);
            item.Init(seed.ItemData.Sprite, seed.Count, seed.ItemData);
            item.SetButtonAction(() => SelectSeed(seed.ItemData));
            item.SetButtonAction(() => OnSeedSelected(seed.ItemData.Sprite));
        }
    }
    
    private void OnDisable()
    {
        foreach (Transform child in _contentPanel.transform)
        {
            Destroy(child.gameObject);
        }
    }

    private void SelectSeed(ItemSO seed) => InteractionManager.Instance.SelectedSeed = seed;

    protected virtual void OnSeedSelected(Sprite e)
    {
        SeedSelected?.Invoke(this, e);
    }
}
