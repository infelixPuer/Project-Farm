using _Scripts.Player.Interaction;
using UnityEngine;

namespace _Scripts.Instruments
{
    public abstract class InstrumentBase : MonoBehaviour, IInteractable
    {
        [SerializeField]
        protected string _name;

        [SerializeField] 
        protected GameObject _object;
        
        [SerializeField] 
        protected float _maxDurability;
        
        [SerializeField] 
        protected float _durability;
        
        [SerializeField] 
        protected float _durabilityLossPerUse;
        
        [SerializeField] 
        protected float _range;

        public abstract void Use();
        
        #region Interaction

        public void Interact()
        {
            Use();
        }

        public void Interact(RaycastHit hitInfo) { }

        #endregion
    }
}