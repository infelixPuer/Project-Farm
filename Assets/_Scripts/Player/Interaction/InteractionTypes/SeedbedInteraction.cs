using System;
using UnityEngine;

namespace _Scripts.Player.Interaction.InteractionTypes
{
    public class SeedbedInteraction : MonoBehaviour, IInteractable
    {
        public void Interact()
        {
            
        }

        public void Interact(RaycastHit hitInfo)
        {
            switch (InteractionManager.Instance.interactionState)
            {   
                case InteractionState.MakingSeedbed:
                    return;
                case InteractionState.Planting:
                    PlantCrop(hitInfo);
                    break;
                case InteractionState.Watering:
                    WaterSeedbed(hitInfo);
                    break;
            }
        }
        
        private void PlantCrop(RaycastHit hitInfo)
        {
            if (InteractionManager.Instance.interactionState != InteractionState.Planting) return;
            
            if (!hitInfo.collider.transform.parent.TryGetComponent<Seedbed>(out var seedbed)) return;
            
            if (seedbed.State != TileState.Empty) return;

            try
            {
                seedbed.PlantCrop(InteractionManager.Instance.SelectedCrop);
                seedbed.UpdateTileState(TileState.Occupied);
            }
            catch (NullReferenceException)
            {
                Debug.LogError("Crop is not selected!");
            }   
        }

        private void WaterSeedbed(RaycastHit hitInfo)
        {
            if (InteractionManager.Instance.interactionState != InteractionState.Watering) return;
            
            if (!hitInfo.collider.transform.parent.TryGetComponent<Seedbed>(out var seedbed)) return;

            seedbed.WaterSeedbed();
        }
    }
}