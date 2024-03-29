﻿using System;
using UnityEngine;

namespace _Scripts.Crops.CropStates
{
    public class CropSlowGrowingState : CropBaseState
    {
        private CropBase _crop;
        private DateTime _dateOfEnteringState;
        private TimeSpan _elapsedTimeSinceEnteringState;
        private TimeSpan _initialTimeOfGrowing;
        private TimeSpan _timeOfGrowingThatLeftSinceEnteringState;
        private Vector3 _cropScaleOnEnteringState;
        private float _currentWaterLevel;
        private float _t;

        public override void EnterCropState(CropStateMachine stateMachine)
        {
            Debug.LogWarning("Enter Slow Growing State");

            _initialTimeOfGrowing = TimeSpan.FromDays(stateMachine.GetCrop().GrowthTime);
            _dateOfEnteringState = TimeManager.Instance.GetCurrentTime();
            _timeOfGrowingThatLeftSinceEnteringState = _initialTimeOfGrowing - (_dateOfEnteringState - stateMachine.PlantedDate);
            _cropScaleOnEnteringState = stateMachine.transform.localScale;
        }

        public override void UpdateCropState(CropStateMachine stateMachine)
        {
            _crop = stateMachine.GetCrop();
            _currentWaterLevel = stateMachine.GetCrop().GetParentSeedbed().GetCurrentWaterLevel();
            
            if (TimeManager.Instance.GetCurrentTime() - stateMachine.PlantedDate >= _initialTimeOfGrowing)
            {
                stateMachine.TransitionToState(stateMachine.CropReadyToHarvestState);
                return;
            }

            if (_currentWaterLevel > stateMachine.GetCrop().MinimalWaterLevel)
            {
                stateMachine.TransitionToState(stateMachine.CropGrowingState);
                return;
            }

            if (_currentWaterLevel <= 0f)
            {
                stateMachine.TransitionToState(stateMachine.CropWiltingState);
                return;
            }
            
            _elapsedTimeSinceEnteringState = TimeManager.Instance.GetCurrentTime() - _dateOfEnteringState;
            _t = (float)(_elapsedTimeSinceEnteringState / _timeOfGrowingThatLeftSinceEnteringState);
            var k = _currentWaterLevel / _crop.MinimalWaterLevel * 0.5f + 0.5f;
            
            _crop.SetCropQuality(k * k - k + 1f);
            stateMachine.transform.localScale = Mathf.Lerp(_cropScaleOnEnteringState.x, 1f, _t * k) * Vector3.one;
            stateMachine.transform.position = new Vector3(stateMachine.transform.position.x, stateMachine.transform.localScale.y * 0.5f + 0.05f, stateMachine.transform.position.z);
        }
    }
}