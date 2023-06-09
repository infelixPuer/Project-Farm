using _Scripts.Player.Inventory;
using _Scripts.UI;
using UnityEngine;

public class InventoryLoaderUI : MonoBehaviour
{
    [SerializeField] 
    private PlayerInventory _playerInventory;

    [SerializeField] 
    private ItemUI _itemUIPrefab;

    private void OnEnable()
    {
        var inventory = _playerInventory.GetInventoryItems();

        for (int i = 0; i < inventory.Length; i++)
        {
            if (inventory[i].IsEmpty)
            {
                Instantiate(_itemUIPrefab.Init(null, null, null), gameObject.transform);
                continue;
            }
            
            var itemObject = _itemUIPrefab.Init(inventory[i].ItemData.Sprite, inventory[i].Count, inventory[i].ItemData);
            
            Instantiate(itemObject, gameObject.transform);
        }
    }

    private void OnDisable()
    {
        foreach (Transform child in transform)
            Destroy(child.gameObject);
    }
}
