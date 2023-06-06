using System;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class CropGrowingState : CropBaseState
{
    private MeshFilter _filter;
    private MeshRenderer _renderer;
    
    private Vector3 _cropStartingScale;
    private TimeSpan _timeOfGrowing;
    private DateTime _dateOfPlanting;
    private TimeSpan _elapsedTime;
    
    public override void EnterCropState(CropStateMachine crop)
    {
        _cropStartingScale = 0.1f * Vector3.one;
        _dateOfPlanting = TimeManager.Instance.GetCurrentTime();
        
        _timeOfGrowing = TimeSpan.FromDays(Crop.GrowthTime);
        
        crop.gameObject.transform.localScale = _cropStartingScale;
    }

    public override void UpdateCropState(CropStateMachine crop)
    {
        if (crop.gameObject.transform.localScale.x >= 1f)
        {
            crop.TransitionToState(crop.CropReadyToHarvestState);
            return;
        }
        
        _elapsedTime = TimeManager.Instance.GetCurrentTime() - _dateOfPlanting ;
        var t = _elapsedTime / _timeOfGrowing;

        crop.gameObject.transform.localScale = Mathf.Lerp(_cropStartingScale.x, 1f, (float)t) * Vector3.one;
        crop.gameObject.transform.position = new Vector3(crop.gameObject.transform.position.x, crop.gameObject.transform.localScale.y * 0.5f + 0.05f, crop.gameObject.transform.position.z);
    }
}
