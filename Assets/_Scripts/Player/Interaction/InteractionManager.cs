using _Scripts.Player.Inventory;
using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    public Camera Cam { get; private set; }
    
    public static InteractionManager Instance;

    public ItemSO SelectedSeed;
    public bool IsSeedSelected => SelectedSeed != null;
    public bool IsShowingUI;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }
        
        Cam = Camera.main;
    }
}
