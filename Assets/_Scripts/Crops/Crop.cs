using UnityEngine;

public class Crop : MonoBehaviour
{
    public CropScriptableObject CropSO;

    private MeshFilter _filter;
    private MeshRenderer _renderer;
    private Seedbed _parentSeedbed;

    private void Awake()
    {
        _filter = GetComponent<MeshFilter>();
        _renderer = GetComponent<MeshRenderer>();
        
        CropSO = InteractionManager.Instance.SelectedCrop;
        
        gameObject.name = CropSO.Name;
        
        Plant();
    }

    private void Plant()
    {
        _filter.sharedMesh = CropSO.PhasesOfGrowing[0].GetComponent<MeshFilter>().sharedMesh;
        _renderer.sharedMaterial = CropSO.PhasesOfGrowing[0].GetComponent<MeshRenderer>().sharedMaterial;
        transform.localScale = CropSO.PhasesOfGrowing[0].transform.localScale;
    }

    public void SetParentSeedbed(Seedbed seedbed) => _parentSeedbed = seedbed;

    public Seedbed GetParentSeedbed() => _parentSeedbed;
}
