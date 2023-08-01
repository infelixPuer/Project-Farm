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
        
        [SerializeField]
        protected Rigidbody _rigidbody;

        public abstract void Use();

        public void Interact(Interactor interactor)
        {
            interactor.GetItemInHand(this);
        }

        public void SetGravity(bool value)
        {
            _rigidbody.useGravity = value;
        }
    }
}