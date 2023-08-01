using UnityEngine;

namespace _Scripts.Instruments
{
    public class Hoe : InstrumentBase
    {
        [SerializeField]
        private LayerMask _terrainMask;
        
        public override void MainAction()
        {
            var camTransform = InteractionManager.Instance.Cam.transform;
            var ray = new Ray(camTransform.position, camTransform.forward);
            
            Physics.Raycast(ray, out var hitInfo, _range, _terrainMask);

            if (hitInfo.collider is null)
                return;

            WorldMap.Instance.InstantiateSeedbed(hitInfo.point);
        }

        public override void SecondaryAction()
        {
            var camTransform = InteractionManager.Instance.Cam.transform;
            var ray = new Ray(camTransform.position, camTransform.forward);
            
            Physics.Raycast(ray, out var hitInfo, _range);

            if (hitInfo.collider is null)
                return;

            var seedbed = hitInfo.collider.GetComponentInParent<Seedbed>();
            
            if (seedbed is null)
                return;
            
            WorldMap.Instance.RemoveSeedbed(seedbed);
        }
    }
}