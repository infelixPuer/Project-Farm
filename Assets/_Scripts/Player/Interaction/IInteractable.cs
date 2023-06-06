using UnityEngine;

namespace _Scripts.Player.Interaction
{
    public interface IInteractable
    {
        public void Interact();
        public void Interact(RaycastHit hitInfo);
    }
}