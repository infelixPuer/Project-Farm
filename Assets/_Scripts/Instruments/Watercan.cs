using UnityEngine;

namespace _Scripts.Instruments
{
    public class Watercan : InstrumentBase
    {
        public override void MainAction()
        {
            var camTransform = InteractionManager.Instance.Cam.transform;
            var ray = new Ray(camTransform.position, camTransform.forward);
            
            Physics.Raycast(ray, out var hitInfo, _range);

            if (hitInfo.collider is null)
                return;

            var seedbed = hitInfo.collider.GetComponentInParent<Seedbed>();
            
            if (seedbed is not null)
            {
                seedbed.WaterSeedbed();
            }
        }
        
        public override void SecondaryAction()
        {
            Debug.Log("Watercan's secondary action");
        }
    }
}