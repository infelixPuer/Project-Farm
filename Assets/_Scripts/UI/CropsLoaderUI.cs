using System.Collections.Generic;
using System.Linq;
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

    private List<CropScriptableObject> _crops;

    private void Awake()
    {
        _crops = Resources.LoadAll<CropScriptableObject>("Scriptables/Crops").ToList();

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

    private void OnCropSelected(CropScriptableObject crop)
    {
        InteractionManager.Instance.SelectedCrop = crop;
    }
}
