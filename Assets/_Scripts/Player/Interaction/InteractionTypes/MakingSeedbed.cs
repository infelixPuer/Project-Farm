using UnityEngine;

namespace _Scripts.Player.Interaction.InteractionTypes
{
    public class MakingSeedbed : MonoBehaviour, IInteractable
    {
        public void Interact() { }

        public void Interact(RaycastHit hitInfo)
        {
            if (InteractionManager.Instance.interactionState != InteractionState.MakingSeedbed) return;
            
            WorldMap.Instance.InstantiateSeedbed(hitInfo.point);
        }
    }
}