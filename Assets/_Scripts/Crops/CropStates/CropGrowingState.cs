using System;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class CropGrowingState : CropBaseState
{
    private Vector3 _cropStartingScale;
    private TimeSpan _intialTimeOfGrowing;
    private TimeSpan _timeOfGrowingThatLeftSinceEnteringState;
    private DateTime _dateOfEnteringState;
    private TimeSpan _elapsedTimeSinceEnteringState;
    private float _t;
    private float _maxScale;
    
    public override void EnterCropState(CropStateMachine stateMachine)
    {
        _cropStartingScale = stateMachine.transform.localScale;
        _dateOfEnteringState = TimeManager.Instance.GetCurrentTime();
        
        _intialTimeOfGrowing = TimeSpan.FromDays(stateMachine.GetCrop().GrowthTime);
        _timeOfGrowingThatLeftSinceEnteringState = _intialTimeOfGrowing - (_dateOfEnteringState - stateMachine.PlantedDate);

        var initialGrowth = Mathf.Lerp(0.1f, 1f, (float)((TimeManager.Instance.GetCurrentTime() - stateMachine.PlantedDate) / _intialTimeOfGrowing));
        var currentGrowth = Mathf.Lerp(_cropStartingScale.x, 1f, (float)((TimeManager.Instance.GetCurrentTime() - _dateOfEnteringState) / _timeOfGrowingThatLeftSinceEnteringState));
        
        _maxScale = 1f - (initialGrowth - currentGrowth);
        
        stateMachine.transform.localScale = _cropStartingScale;
        Debug.Log(TimeManager.Instance.GetCurrentTime());
    }

    public override void UpdateCropState(CropStateMachine stateMachine)
    {
        var crop = stateMachine.GetCrop();
        
        if (_elapsedTimeSinceEnteringState >= _intialTimeOfGrowing)
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

        _elapsedTimeSinceEnteringState = TimeManager.Instance.GetCurrentTime() - _dateOfEnteringState;
        _t = (float)(_elapsedTimeSinceEnteringState / _timeOfGrowingThatLeftSinceEnteringState);

        stateMachine.gameObject.transform.localScale = Mathf.Lerp(_cropStartingScale.x, _maxScale, _t) * Vector3.one;
        stateMachine.gameObject.transform.position = new Vector3(stateMachine.gameObject.transform.position.x, stateMachine.gameObject.transform.localScale.y * 0.5f + 0.05f, stateMachine.gameObject.transform.position.z);
    }
}
