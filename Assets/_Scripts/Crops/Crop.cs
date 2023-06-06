using _Scripts.Player.Inventory;
using UnityEngine;

public class Crop : MonoBehaviour
{
    public CropScriptableObject CropSO;
    public ItemSO Item;

    private MeshFilter _filter;
    private MeshRenderer _renderer;
    private Seedbed _parentSeedbed;

    private void Awake()
    {
        _filter = GetComponent<MeshFilter>();
        _renderer = GetComponent<MeshRenderer>();
        
        Item = InteractionManager.Instance.SelectedCrop;
        
        gameObject.name = Item.Name;
        
        Plant();
    }

    private void Plant()
    {
        _filter.sharedMesh = Item.Object.GetComponent<MeshFilter>().sharedMesh;
        _renderer.sharedMaterial = Item.Object.GetComponent<MeshRenderer>().sharedMaterial;
        transform.localScale = Item.Object.transform.localScale;
    }

    public void SetParentSeedbed(Seedbed seedbed) => _parentSeedbed = seedbed;

    public Seedbed GetParentSeedbed() => _parentSeedbed;
}
