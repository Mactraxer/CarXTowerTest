using System;
using System.Collections.Generic;

public class TowerStateMachine
{
    private readonly Dictionary<Type, ITowerState> _states = new();
    private ITowerState _currentState;

    public void AddState<T>(T state) where T : ITowerState
    {
        _states[typeof(T)] = state;
    }

    public void ChangeState<T>() where T : ITowerState
    {
        _currentState?.Exit();
        _currentState = _states[typeof(T)];
        _currentState.Enter();
    }

    public void Tick()
    {
        _currentState?.Tick();
    }
}
