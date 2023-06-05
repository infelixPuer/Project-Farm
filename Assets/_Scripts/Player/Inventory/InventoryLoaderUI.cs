using _Scripts.Player.Inventory;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryLoaderUI : MonoBehaviour
{
    [SerializeField] 
    private PlayerInventory _playerInventory;

    [SerializeField] 
    private GameObject _itemUIPrefab;

    private void OnEnable()
    {
        var inventory = _playerInventory.GetInventory();

        for (int i = 0; i < inventory.Length; i++)
        {
            if (inventory[i].IsEmpty)
                continue;
            
            var itemObject = _itemUIPrefab;
            itemObject.GetComponentInChildren<Image>().sprite = inventory[i].ItemData.Sprite;
            var TMPs = itemObject.GetComponentsInChildren<TextMeshProUGUI>();

            foreach (var text in TMPs)
            {
                if (text.gameObject.name == "ItemCountText")
                {
                    text.text = inventory[i].Count.ToString();
                    break;
                }
            }
            
            Instantiate(itemObject, gameObject.transform);
        }
    }

    private void OnDisable()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }
}
