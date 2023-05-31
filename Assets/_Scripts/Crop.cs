using System;
using UnityEngine;

public class Crop : MonoBehaviour
{
    private CropScriptableObject _crop;
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
        
        _crop = InteractionManager.Instance.SelectedCrop;

        Debug.Assert(_crop.GrowingTime != _crop.PhasesOfGrowing.Length + 1, "There is not enough phases of growing to fulfill growing time!");
        
        gameObject.name = _crop.Name;
        _growingTime = _crop.GrowingTime;
        _frequencyOfWatering = _crop.FrequencyOfWateringInDays;
        _output = _crop.Output;
        
        Plant();
    }

    private void Plant()
    {
        _growingStage = 0;
        _filter.sharedMesh = _crop.PhasesOfGrowing[_growingStage].GetComponent<MeshFilter>().sharedMesh;
        _renderer.sharedMaterial = _crop.PhasesOfGrowing[_growingStage].GetComponent<MeshRenderer>().sharedMaterial;
        transform.localScale = _crop.PhasesOfGrowing[_growingStage].transform.localScale;
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
        _filter.sharedMesh = _crop.PhasesOfGrowing[_growingStage].GetComponent<MeshFilter>().sharedMesh;
        _renderer.sharedMaterial = _crop.PhasesOfGrowing[_growingStage].GetComponent<MeshRenderer>().sharedMaterial;
        transform.localScale = _crop.PhasesOfGrowing[_growingStage].transform.localScale;
        transform.position = new Vector3(transform.position.x, transform.localScale.y * 0.5f + 0.05f, transform.position.z);
    }

    public void SetParentSeedbed(Seedbed seedbed)
    {
        _parentSeedbed = seedbed;
    }
}
