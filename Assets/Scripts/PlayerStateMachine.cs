using UnityEngine;
using Debug = System.Diagnostics.Debug;

public class PlayerStateMachine
{
    private PlayerState _currentState;

    public PlayerState CurrentState
    {
        get => _currentState;
        set
        {
            _currentState?.Exit();
            _currentState = value;
            _currentState?.Enter();
        }
    }
}