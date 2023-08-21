using System.Collections;
using _Scripts.Player.Inventory;
using _Scripts.UI;
using _Scripts.World;
using UnityEngine;

namespace _Scripts.Instruments
{
    public class Bag : InstrumentBase
    {
        [Header("Bag specific")]
        [SerializeField]
        private Canvas _selectingSeedsCanvas;
        
        [SerializeField]
        private SeedsLoaderUI _seedsLoaderUI;

        [SerializeField] 
        private Material _material;
        
        private bool _isSelectingSeeds;

        private void Awake()
        {
            _seedsLoaderUI.SeedSelected += UpdateBagMaterial;
        }

        public override void MainAction()
        {
            var camTransform = InteractionManager.Instance.Cam.transform;
            var ray = new Ray(camTransform.position, camTransform.forward);
            
            Physics.Raycast(ray, out var hitInfo, _range);

            if (hitInfo.collider is null)
                return;
            
            var seedbed = hitInfo.collider.GetComponentInParent<Seedbed>();
            
            if (seedbed is null)
                return;

            if (InteractionManager.Instance.SelectedSeed is null)
                return;
            
            seedbed.PlantCrop(InteractionManager.Instance.SelectedSeed);
            seedbed.UpdateTileState(TileState.Occupied);
            PlayerInventory.Instance.RemoveItem(InteractionManager.Instance.SelectedSeed, 1);

            if (!PlayerInventory.Instance.CheckRequiredItems(new[] { new Item(InteractionManager.Instance.SelectedSeed, 1) }))
            {
                _material.mainTexture = null;
                InteractionManager.Instance.SelectedSeed = null;
            }
        }

        public override void SecondaryAction()
        {
            UIManager.Instance.ShowCanvas(_selectingSeedsCanvas);
            StartCoroutine(CloseCanvas());
        }

        public override void ResetObject(bool gravityValue)
        {
            base.ResetObject(gravityValue);
            _material.mainTexture = null;
            InteractionManager.Instance.SelectedSeed = null;
        }

        private IEnumerator CloseCanvas()
        {
            yield return new WaitUntil(() => Input.GetMouseButtonUp(1));
            UIManager.Instance.HideCanvas(_selectingSeedsCanvas);
        }

        public void UpdateBagMaterial(object sender, Sprite sprite)
        {
            _material.mainTexture = sprite.texture;
        }
    }
}