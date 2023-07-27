using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region Component

    public Animator Animator { get; private set; }

    #endregion

    #region State

    public PlayerStateMachine StateMachine { get; private set; }
    public PlayerIdleState PlayerIdleState { get; private set; }
    public PlayerMoveState PlayerMoveState { get; private set; }

    #endregion

    private void Awake()
    {
        StateMachine = new PlayerStateMachine();

        PlayerIdleState = new PlayerIdleState(StateMachine, this, "Idle");
        PlayerMoveState = new PlayerMoveState(StateMachine, this, "Move");
    }

    private void Start()
    {
        Animator = GetComponentInChildren<Animator>();
        StateMachine.CurrentState = PlayerIdleState;
    }

    private void Update()
    {
        StateMachine.CurrentState.Update();
    }
}