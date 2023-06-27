using UnityEngine;

namespace _Scripts.Player.Interaction.InteractionTypes
{
    public class WaterSeedbed : MonoBehaviour, IInteractable
    {
        public void Interact() { }

        public void Interact(RaycastHit hitInfo)
        {
            if (InteractionManager.Instance.interactionState != InteractionState.Watering) return;
            
            if (!hitInfo.collider.transform.parent.TryGetComponent<Seedbed>(out var seedbed)) return;

            seedbed.WaterSeedbed();
        }
    }
}