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

    private List<ItemSO> _crops;

    private void Awake()
    {
        _crops = Resources.LoadAll<ItemSO>("Scriptables/InventoryItems/Food/").ToList();

        foreach (var crop in _crops)
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
                tmp.text = crop.Name;
            }
            
            button.onClick.AddListener(() => OnCropSelected(crop));
        }
        
        _canvas.gameObject.SetActive(false);
    }

    private void OnCropSelected(ItemSO crop)
    {
        InteractionManager.Instance.SelectedCrop = crop;
    }
}
