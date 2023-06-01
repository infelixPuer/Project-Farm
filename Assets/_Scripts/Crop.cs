using System;
using UnityEngine;
using UnityEngine.Serialization;

public class Crop : MonoBehaviour
{
    [FormerlySerializedAs("_crop")] public CropScriptableObject CropSO;
    private int _growingTime;
    private int _frequencyOfWatering;
    private int _output;

    private MeshFilter _filter;
    private MeshRenderer _renderer;
    private Seedbed _parentSeedbed;
    private int _growingStage;

    private void Awake()
    {
        _filter = GetComponent<MeshFilter>();
        _renderer = GetComponent<MeshRenderer>();
        
        CropSO = InteractionManager.Instance.SelectedCrop;

        Debug.Assert(CropSO.GrowingTime != CropSO.PhasesOfGrowing.Length + 1, "There is not enough phases of growing to fulfill growing time!");
        
        gameObject.name = CropSO.Name;
        _growingTime = CropSO.GrowingTime;
        _frequencyOfWatering = CropSO.FrequencyOfWateringInDays;
        _output = CropSO.Output;
        
        Plant();
    }

    private void Plant()
    {
        _growingStage = 0;
        _filter.sharedMesh = CropSO.PhasesOfGrowing[_growingStage].GetComponent<MeshFilter>().sharedMesh;
        _renderer.sharedMaterial = CropSO.PhasesOfGrowing[_growingStage].GetComponent<MeshRenderer>().sharedMaterial;
        transform.localScale = CropSO.PhasesOfGrowing[_growingStage].transform.localScale;
    }

    public void Grow()
    {
        if (_growingStage == _growingTime)
        {
            Debug.Log("Crop is ready to harvest!");
            return;
        }

        if (!_parentSeedbed.GetWateredStatus())
        {
            Debug.LogWarning("Seedbed is not watered!\nCrop can't grow!");
            return;
        }
        
        _parentSeedbed.DrySeedbed();
        ++_growingStage;
        _filter.sharedMesh = CropSO.PhasesOfGrowing[_growingStage].GetComponent<MeshFilter>().sharedMesh;
        _renderer.sharedMaterial = CropSO.PhasesOfGrowing[_growingStage].GetComponent<MeshRenderer>().sharedMaterial;
        transform.localScale = CropSO.PhasesOfGrowing[_growingStage].transform.localScale;
        transform.position = new Vector3(transform.position.x, transform.localScale.y * 0.5f + 0.05f, transform.position.z);
    }

    public void SetParentSeedbed(Seedbed seedbed)
    {
        _parentSeedbed = seedbed;
    }
}
