using _Scripts.DialogSystem;
using _Scripts.Player.Interaction;
using UnityEngine;

public class NPC : MonoBehaviour, IInteractable
{
    [SerializeField]
    private TextAsset _inkJsonAsset;
    
    public void Interact()
    {
        DialogManager.Instance.StartDialog(_inkJsonAsset);
    }

    public void Interact(RaycastHit hitInfo) { }
}
