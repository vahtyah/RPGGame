using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Move Info")] public float moveSpeed = 8f;
    #region Component

    public Animator Animator { get; private set; }
    public Rigidbody2D rb { get; private set; }

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
        rb = GetComponent<Rigidbody2D>();
        StateMachine.CurrentState = PlayerIdleState; 
    }

    private void Update()
    {
        StateMachine.CurrentState.Update();
    }

    public void SetVelocity(float xVelocity, float yVelocity)
    {
        rb.velocity = new Vector2(xVelocity, yVelocity);
    }
}