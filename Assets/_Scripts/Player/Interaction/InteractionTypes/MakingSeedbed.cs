using UnityEngine;

namespace _Scripts.Player.Interaction.InteractionTypes
{
    public class MakingSeedbed : MonoBehaviour, IInteractable
    {
        public void Interact(Interactor interactor)
        {
            if (InteractionManager.Instance.interactionState != InteractionState.MakingSeedbed) return;
            
            WorldMap.Instance.InstantiateSeedbed(interactor.HitInfo.point);
        }
    }
}