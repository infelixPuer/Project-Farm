using System.Collections.Generic;
using System.Linq;
using _Scripts.Player.Inventory;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CropsLoaderUI : MonoBehaviour
{
    [SerializeField] 
    private Canvas _canvas;
    
    [SerializeField] 
    private GameObject _panel;

    [SerializeField] 
    private GameObject _panelItemPrefab;

    private List<ItemSO> _seeds;

    private void Awake()
    {
        _seeds = Resources.LoadAll<ItemSO>("Scriptables/InventoryItems/Seeds/").ToList();

        foreach (var seed in _seeds)
        {
            var item = Instantiate(_panelItemPrefab, _panel.transform);
            var button = item.GetComponentInChildren<Button>();
            var tmp = button.gameObject.GetComponentInChildren<TMP_Text>();

            if (tmp == null)
            {
                Debug.LogError("Tmp is null");
            }
            else
            {
                tmp.text = seed.Name;
            }
            
            button.onClick.AddListener(() => OnSeedSelected(seed));
        }
    }

    private void OnSeedSelected(ItemSO seed)
    {
        InteractionManager.Instance.SelectedSeed = seed;
    }
}
