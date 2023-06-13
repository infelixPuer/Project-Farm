using System;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class CropGrowingState : CropBaseState
{
    private Vector3 _cropStartingScale;
    private TimeSpan _timeOfGrowing;
    private DateTime _dateOfPlanting;
    private TimeSpan _elapsedTime;
    private float _t;
    
    public override void EnterCropState(CropStateMachine stateMachine)
    {
        _cropStartingScale = 0.1f * Vector3.one;
        _dateOfPlanting = stateMachine.PlantedDate;
        
        _timeOfGrowing = TimeSpan.FromDays(stateMachine.GetCrop().GrowthTime);
        
        stateMachine.transform.localScale = _cropStartingScale;
        Debug.Log(TimeManager.Instance.GetCurrentTime());
    }

    public override void UpdateCropState(CropStateMachine stateMachine)
    {
        var crop = stateMachine.GetCrop();
        
        if (_elapsedTime >= _timeOfGrowing)
        {
            stateMachine.TransitionToState(stateMachine.CropReadyToHarvestState);
            Debug.Log(TimeManager.Instance.GetCurrentTime());
            return;
        }

        if (crop.GetParentSeedbed().GetCurrentWaterLevel() <= crop.MinimalWaterLevel)
        {
            stateMachine.TransitionToState(stateMachine.CropSlowGrowingState);
            return;
        }

        _elapsedTime = TimeManager.Instance.GetCurrentTime() - _dateOfPlanting;
        _t = (float)(_elapsedTime / _timeOfGrowing);

        stateMachine.gameObject.transform.localScale = Mathf.Lerp(_cropStartingScale.x, 1f, _t) * Vector3.one;
        stateMachine.gameObject.transform.position = new Vector3(stateMachine.gameObject.transform.position.x, stateMachine.gameObject.transform.localScale.y * 0.5f + 0.05f, stateMachine.gameObject.transform.position.z);
    }
}
