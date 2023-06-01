using System;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class CropGrowingState : CropBaseState
{
    private Vector3 _cropStartingScale;
    // private Vector3 _cropEndingScale;
    // private Vector3 _cropScaling = new Vector3(0.1f, 0.1f, 0.1f);
    private TimeSpan _timeOfGrowing;
    private DateTime _dateOfPlanting;
    private TimeSpan _elapsedTime;
    
    public override void EnterCropState(CropStateMachine stateMachine)
    {
        _cropStartingScale = 0.1f * Vector3.one;
        // _cropEndingScale = Crop.CropSO.PhasesOfGrowing[Crop.CropSO.PhasesOfGrowing.Length - 1].transform.localScale;
        _dateOfPlanting = TimeManager.Instance.GetCurrentTime();
        
        _timeOfGrowing = TimeSpan.FromDays(Crop.CropSO.GrowingTime);
        
        stateMachine.gameObject.transform.localScale = _cropStartingScale;
    }

    public override void UpdateCropState(CropStateMachine stateMachine)
    {
        if (stateMachine.gameObject.transform.localScale.x >= 1f) return;
        
        _elapsedTime = TimeManager.Instance.GetCurrentTime() - _dateOfPlanting ;
        var t = _elapsedTime / _timeOfGrowing;

        stateMachine.gameObject.transform.localScale = Mathf.Lerp(_cropStartingScale.x, 1f, (float)t) * Vector3.one;
        //stateMachine.transform.localScale += _cropScaling * Time.deltaTime;
        stateMachine.gameObject.transform.position = new Vector3(stateMachine.gameObject.transform.position.x, stateMachine.gameObject.transform.localScale.y * 0.5f + 0.05f, stateMachine.gameObject.transform.position.z);
    }
}
