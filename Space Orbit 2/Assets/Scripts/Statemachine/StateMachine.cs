using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    BaseState _currentState;
    public BaseState CurrentState { get { return _currentState; } }

    BaseState _previousState;
    public BaseState PreviousState { get { return _previousState; } }

    public void ExecuteState()
    {
        if (_currentState == null) { return; }

        _currentState.Execute();
    }

    public void ChangeState(BaseState newState)
    {
        if(_currentState != null)
        {
            _currentState.Exit();
        }

        _previousState = _currentState;
        _currentState = newState;
        _currentState.Enter();
    }

    public void ChangeToPreviousState()
    {
        if(_previousState == null)
        {
            Debug.Log("The previous state is empty");
            return;
        }

        ChangeState(_previousState);
    }
}
