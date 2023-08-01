using UnityEngine;

namespace _Scripts.Instruments
{
    public class Hoe : InstrumentBase
    {
        [SerializeField]
        private LayerMask _terrainMask;
        
        public override void Use()
        {
            var camTransform = InteractionManager.Instance.Cam.transform;
            var ray = new Ray(camTransform.position, camTransform.forward);
            
            Physics.Raycast(ray, out var hitInfo, _range, _terrainMask);

            if (hitInfo.collider is null)
                return;

            WorldMap.Instance.InstantiateSeedbed(hitInfo.point);
        }
    }
}