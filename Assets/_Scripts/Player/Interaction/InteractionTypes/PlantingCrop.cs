﻿using System;
using UnityEngine;

namespace _Scripts.Player.Interaction.InteractionTypes
{
    public class PlantingCrop : MonoBehaviour, IInteractable
    {
        public void Interact() { }

        public void Interact(RaycastHit hitInfo)
        {
            if (InteractionManager.Instance.interactionState != InteractionState.Planting) return;
            
            if (!hitInfo.collider.transform.parent.TryGetComponent<Seedbed>(out var seedbed)) return;
            
            if (seedbed.State != TileState.Empty) return;

            try
            {
                seedbed.PlantCrop(InteractionManager.Instance.SelectedSeed);
                seedbed.UpdateTileState(TileState.Occupied);
            }
            catch (NullReferenceException)
            {
                Debug.LogError("Crop is not selected!");
            }
        }
    }
}