using UnityEngine;

namespace _Scripts.Player.Interaction.InteractionTypes
{
    public class WaterSeedbed : MonoBehaviour, IInteractable
    {
        public void Interact(Interactor interactor)
        {
            if (InteractionManager.Instance.interactionState != InteractionState.Watering) return;
            
            if (!interactor.HitInfo.collider.transform.parent.TryGetComponent<Seedbed>(out var seedbed)) return;

            seedbed.WaterSeedbed();
        }
    }
}