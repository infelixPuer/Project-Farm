using System;
using UnityEngine;

public class CropStateMachine : MonoBehaviour
{
    private CropBaseState _currentState;
    private CropGrowingState _cropGrowingState = new();

    private Crop _crop;

    private void Awake()
    {
        _crop = GetComponent<Crop>();
    }

    private void Start()
    {
        _currentState = _cropGrowingState;
        _currentState.Crop = _crop;
        _currentState.EnterCropState(this);
    }

    private void Update()
    {
        _currentState.UpdateCropState(this);
    }

    public void TransitionToState(CropBaseState state)
    {
        _currentState = state;
        _currentState.EnterCropState(this);
    }
}
