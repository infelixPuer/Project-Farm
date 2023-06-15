using System;
using UnityEngine;

namespace _Scripts.Crops.CropStates
{
    public class CropWiltingState : CropBaseState
    {
        private CropBase _crop;
        private DateTime _dateOfEnteringState;
        private TimeSpan _elapsedTimeSinceEnteringState;
        private TimeSpan _initialTimeOfGrowing;
        private TimeSpan _timeOfGrowingThatLeftSinceEnteringState;
        private Vector3 _cropScaleOnEnteringState;
        private float _currentWaterLevel;
        private float _initialCropQuality;
        private float _t;
        
        private float _wiltingScale = 0.5f;
        
        public override void EnterCropState(CropStateMachine stateMachine)
        {
            Debug.Log("Enter Wilting State");
            
            _crop = stateMachine.GetCrop();
            _dateOfEnteringState = TimeManager.Instance.GetCurrentTime();
            _initialTimeOfGrowing = TimeSpan.FromDays(stateMachine.GetCrop().GrowthTime);
            _timeOfGrowingThatLeftSinceEnteringState = _initialTimeOfGrowing - (_dateOfEnteringState - stateMachine.PlantedDate);
            _cropScaleOnEnteringState = stateMachine.transform.localScale;
            _initialCropQuality = _crop.GetCropQuality();
            
            Debug.Log($"Crop scale: {_cropScaleOnEnteringState.x}");
        }

        public override void UpdateCropState(CropStateMachine stateMachine)
        {
            _currentWaterLevel = _crop.GetParentSeedbed().GetCurrentWaterLevel();
            
            if (_currentWaterLevel > 0f)
            {
                stateMachine.TransitionToState(stateMachine.CropSlowGrowingState);
                return;
            }

            if (_t >= 1f)
            {
                Debug.Log($"Crop scale: {stateMachine.transform.localScale.x}");
                stateMachine.TransitionToState(stateMachine.CropDeadState);
                return;
            }
            
            _elapsedTimeSinceEnteringState = TimeManager.Instance.GetCurrentTime() - _dateOfEnteringState;
            _t = (float)(_elapsedTimeSinceEnteringState / _timeOfGrowingThatLeftSinceEnteringState);
            
            _crop.SetCropQuality(Mathf.Lerp(_initialCropQuality, 0f, _t));
            
            stateMachine.transform.localScale = Mathf.Lerp(_cropScaleOnEnteringState.x, _cropScaleOnEnteringState.x * _wiltingScale, _t) * Vector3.one;
            stateMachine.transform.position = new Vector3(stateMachine.transform.position.x, stateMachine.transform.localScale.y * 0.5f + 0.05f, stateMachine.transform.position.z);
        }
    }
}