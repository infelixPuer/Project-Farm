using System;
using UnityEngine;

namespace _Scripts.Crops.CropStates
{
    public class CropSlowGrowingState : CropBaseState
    {
        private CropBase _crop;
        private TimeSpan _elapsedTimeSinceEnteringState;
        private float _currentWaterLevel;
        private TimeSpan _initialTimeOfGrowing;
        private DateTime _dateOfEnteringState;
        private TimeSpan _timeOfGrowingThatLeftSinceEnteringState;
        private Vector3 _cropScaleOnEnteringState;
        private float _t;

        public override void EnterCropState(CropStateMachine stateMachine)
        {
            Debug.Log("Enter Slow Growing State");

            _dateOfEnteringState = TimeManager.Instance.GetCurrentTime();
            _timeOfGrowingThatLeftSinceEnteringState = TimeSpan.FromDays(stateMachine.GetCrop().GrowthTime) - (_dateOfEnteringState - stateMachine.PlantedDate);
            _cropScaleOnEnteringState = stateMachine.transform.localScale;

            _initialTimeOfGrowing = TimeSpan.FromDays(stateMachine.GetCrop().GrowthTime);
        }

        public override void UpdateCropState(CropStateMachine stateMachine)
        {
            _crop = stateMachine.GetCrop();
            _currentWaterLevel = stateMachine.GetCrop().GetParentSeedbed().GetCurrentWaterLevel();
            
            if (TimeManager.Instance.GetCurrentTime() - stateMachine.PlantedDate >= _initialTimeOfGrowing)
            {
                stateMachine.TransitionToState(stateMachine.CropReadyToHarvestState);
                Debug.Log(TimeManager.Instance.GetCurrentTime());
                return;
            }
            
            //
            // if (_currentWaterLevel > crop.GetCrop().MinimalWaterLevel)
            // {
            //     crop.TransitionToState(crop.CropGrowingState);
            //     return;
            // }
            //
            
            if (_currentWaterLevel <= 0f)
            {
                stateMachine.TransitionToState(stateMachine.CropWiltingState);
                return;
            }
            
            _elapsedTimeSinceEnteringState = TimeManager.Instance.GetCurrentTime() - _dateOfEnteringState;
            _t = (float)(_elapsedTimeSinceEnteringState / _timeOfGrowingThatLeftSinceEnteringState);
            var k = Mathf.Lerp(0.5f, 1f, _currentWaterLevel / _crop.MinimalWaterLevel);

            stateMachine.transform.localScale = Mathf.Lerp(_cropScaleOnEnteringState.x, 1f, _t * k) * Vector3.one;
            stateMachine.transform.position = new Vector3(stateMachine.transform.position.x, stateMachine.transform.localScale.y * 0.5f + 0.05f, stateMachine.transform.position.z);
        }
    }
}