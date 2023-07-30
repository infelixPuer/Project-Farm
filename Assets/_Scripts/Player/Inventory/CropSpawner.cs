using _Scripts.Player.Interaction;
using _Scripts.Player.Inventory;
using UnityEngine;

public class CropSpawner : MonoBehaviour, IInteractable
{
    [SerializeField] 
    private ItemSO _item;

    [SerializeField] 
    private PlayerInventory _inventory;
    
    public void Interact(Interactor interactor) => _inventory.AddItem(_item, 1);
}
