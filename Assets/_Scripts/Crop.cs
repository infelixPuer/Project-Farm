using System;
using UnityEngine;

public class Crop : MonoBehaviour
{
    private CropScriptableObject _crop;
    private int _growingTime;
    private int _frequencyOfWatering;
    private int _output;

    private MeshFilter _meshFilter;
    private int _growingStage;

    private void Awake()
    {
        _crop = InteractionManager.Instance.Crop;

        Debug.Assert(_crop.GrowingTime != _crop.PhasesOfGrowing.Length + 1, "There is not enough phases of growing to fulfill growing time!");
        
        gameObject.name = _crop.Name;
        _growingTime = _crop.GrowingTime;
        _frequencyOfWatering = _crop.FrequencyOfWateringInDays;
        _output = _crop.Output;

        _meshFilter = GetComponent<MeshFilter>();

        Plant();
    }

    private void OnEnable()
    {
        
    }

    private void Plant()
    {
        _growingStage = 0;
        _meshFilter.sharedMesh = _crop.PhasesOfGrowing[_growingStage].GetComponent<MeshFilter>().sharedMesh;
        transform.localScale = _crop.PhasesOfGrowing[_growingStage].transform.localScale;
    }

    public void Grow()
    {
        if (_growingStage == _growingTime)
        {
            Debug.Log("Crop is ready to harvest!");
            return;
        }

        ++_growingStage;
        _meshFilter = _crop.PhasesOfGrowing[_growingStage].GetComponent<MeshFilter>();
        transform.localScale = _crop.PhasesOfGrowing[_growingStage].transform.localScale;
        transform.position = new Vector3(transform.position.x, transform.localScale.y * 0.5f + 0.05f, transform.position.z);
    }
}
