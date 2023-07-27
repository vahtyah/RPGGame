﻿using System;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Move Info")] public float moveSpeed = 8f;
    public float jumpForce = 12f;

    [Header("Dash Info")] public float dashCooldown = 1f;
    private float dashUsageTimer;
    public float dashSpeed = 25;
    public float dashDuration = 0.2f;
    public float dashDir { get; private set; }

    [Header("Collision Info")] [SerializeField]
    private Transform _groundCheck;

    [SerializeField] private float _groundCheckDistance;
    [SerializeField] private Transform _wallCheck;
    [SerializeField] private float _wallCheckDistance;
    [SerializeField] private LayerMask _whatIsGround;


    #region Component

    public Animator Animator { get; private set; }
    public Rigidbody2D rb { get; private set; }

    #endregion

    #region State

    public PlayerStateMachine StateMachine { get; private set; }
    public PlayerIdleState PlayerIdleState { get; private set; }
    public PlayerMoveState PlayerMoveState { get; private set; }
    public PlayerJumpState PlayerJumpState { get; private set; }
    public PlayerAirState PlayerAirState { get; private set; }
    public PlayerWallSlideState PlayerWallSlideState { get; private set; }
    public PlayerDashState PlayerDashState { get; private set; }
    public PlayerWallJumpState PlayerWallJumpState { get; private set; }
    public PlayerPrimaryAttackState PlayerPrimaryAttackState { get; private set; }

    #endregion

    private void Awake()
    {
        StateMachine = new PlayerStateMachine();

        PlayerIdleState = new PlayerIdleState(StateMachine, this, "Idle");
        PlayerMoveState = new PlayerMoveState(StateMachine, this, "Move");
        PlayerJumpState = new PlayerJumpState(StateMachine, this, "Jump");
        PlayerAirState = new PlayerAirState(StateMachine, this, "Jump");
        PlayerWallSlideState = new PlayerWallSlideState(StateMachine, this, "WallSlide");
        PlayerDashState = new PlayerDashState(StateMachine, this, "Dash");
        PlayerWallJumpState = new PlayerWallJumpState(StateMachine, this, "Jump");
        PlayerPrimaryAttackState = new PlayerPrimaryAttackState(StateMachine, this, "Attack");
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
        CheckForDashInput();
    }

    public void AnimationTrigger() => StateMachine.CurrentState.AnimationFinishTrigger();

    public void SetVelocity(float xVelocity, float yVelocity)
    {
        rb.velocity = new Vector2(xVelocity, yVelocity);
        Flip();
    }

    private void Flip()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(rb.velocity.x) > Mathf.Epsilon;
        if (playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(rb.velocity.x), 1f);
        }
    }

    private void CheckForDashInput()
    {
        if(IsWallDetected()) return;
        dashUsageTimer -= Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.LeftShift) && dashUsageTimer < 0f)
        {
            dashUsageTimer = dashCooldown;
            dashDir = Input.GetAxisRaw("Horizontal");

            if (dashDir == 0) dashDir = transform.localScale.x;

            StateMachine.CurrentState = PlayerDashState;
        }
    }

    public bool IsGroundDetected() =>
        Physics2D.Raycast(_groundCheck.position, Vector2.down, _groundCheckDistance, _whatIsGround);

    public bool IsWallDetected() => Physics2D.Raycast(_wallCheck.position, Vector2.right * transform.localScale.x,
        _wallCheckDistance, _whatIsGround);

    private void OnDrawGizmos()
    {
        var position = _groundCheck.position;
        Gizmos.DrawLine(position, new Vector3(position.x, position.y - _groundCheckDistance));
        var position1 = _wallCheck.position;
        Gizmos.DrawLine(position1, new Vector3(position1.x + _wallCheckDistance, position1.y));
    }
}