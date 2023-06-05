using _Scripts.Player.Inventory;
using _Scripts.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryLoaderUI : MonoBehaviour
{
    [SerializeField] 
    private PlayerInventory _playerInventory;

    [SerializeField] 
    private InventoryItemUI _itemUIPrefab;

    private void OnEnable()
    {
        var inventory = _playerInventory.GetInventory();

        for (int i = 0; i < inventory.Length; i++)
        {
            if (inventory[i].IsEmpty)
                continue;
            
            var itemObject = _itemUIPrefab;
            itemObject.GetItemSprite().sprite = inventory[i].ItemData.Sprite;
            itemObject.GetTextMeshPro().text = inventory[i].Count.ToString();
            
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
