using System.Collections;
using _Scripts.UI;
using UnityEngine;

namespace _Scripts.Instruments
{
    public class Bag : InstrumentBase
    {
        [Header("Bag specific")]
        [SerializeField]
        private Canvas _selectingSeedsCanvas;

        [SerializeField] 
        private Material _material;
        
        private bool _isSelectingSeeds;
        
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
        }

        public override void SecondaryAction()
        {
            UIManager.Instance.ShowCanvas(_selectingSeedsCanvas);
            StartCoroutine(CloseCanvas());
        }

        private IEnumerator CloseCanvas()
        {
            yield return new WaitUntil(() => Input.GetMouseButtonUp(1));
            UIManager.Instance.HideCanvas(_selectingSeedsCanvas);
        }
    }
}