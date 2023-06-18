using _Scripts.Player.Inventory;
using _Scripts.UI;
using UnityEngine;

public class InventoryLoaderUI : MonoBehaviour
{
    [SerializeField] 
    private PlayerInventory _playerInventory;

    [SerializeField] 
    private InventoryItemUI _itemUIPrefab;

    private void OnEnable()
    {
        var inventory = _playerInventory.GetInventoryItems();

        for (int i = 0; i < inventory.Length; i++)
        {
            if (inventory[i].IsEmpty)
            {
                Instantiate(_itemUIPrefab.Init(null, ""), gameObject.transform);
                continue;
            }
            
            var itemObject = _itemUIPrefab.Init(inventory[i].ItemData.Sprite, inventory[i].Count.ToString());
            
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
