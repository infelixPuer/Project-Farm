using _Scripts.Player.Interaction;
using _Scripts.Player.Inventory;
using UnityEngine;

namespace _Scripts.ConstructionMaterials
{
    public class Stone : MonoBehaviour, IInteractable
    {
        public ItemSO Item;
        public int Amount;

        public void Interact(Interactor interactor)
        {
            if (interactor.TryGetComponent(out PlayerInventory playerInventory))
            {
                playerInventory.AddItem(Item, Amount);
                Destroy(gameObject);
            }
        }
    }
}