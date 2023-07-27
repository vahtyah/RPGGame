using System;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Move Info")] public float moveSpeed = 8f;
    public float jumpForce = 12f;

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

    #endregion

    private void Awake()
    {
        StateMachine = new PlayerStateMachine();

        PlayerIdleState = new PlayerIdleState(StateMachine, this, "Idle");
        PlayerMoveState = new PlayerMoveState(StateMachine, this, "Move");
        PlayerJumpState = new PlayerJumpState(StateMachine, this, "Jump");
        PlayerAirState = new PlayerAirState(StateMachine, this, "Jump");
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

    public bool IsGroundDetected() =>
        Physics2D.Raycast(_groundCheck.position, Vector2.down, _groundCheckDistance, _whatIsGround);

    private void OnDrawGizmos()
    {
        var position = _groundCheck.position;
        Gizmos.DrawLine(position, new Vector3(position.x, position.y - _groundCheckDistance));
        var position1 = _wallCheck.position;
        Gizmos.DrawLine(position1, new Vector3(position1.x + _wallCheckDistance, position1.y));
    }
}