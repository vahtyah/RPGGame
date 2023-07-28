using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

namespace Player
{
    public class Player : Entity
    {
        [Header("Attack details")] 
        public Vector2[] attackMovement;

        
        public bool isBusy { get; private set; }

        [Header("Move Info")] public float moveSpeed = 8f;
        public float jumpForce = 12f;

        [Header("Dash Info")] public float dashCooldown = 1f;
        private float dashUsageTimer;
        public float dashSpeed = 25;
        public float dashDuration = 0.2f;
        public float dashDir { get; private set; }


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

        protected override void Awake()
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

        protected override void Start()
        {
            base.Start();
            StateMachine.CurrentState = PlayerIdleState;
        }

        protected override void Update()
        {
            base.Update();
            StateMachine.CurrentState.Update();
            CheckForDashInput();
        }

        private IEnumerator BusyFor(float second)
        {
            isBusy = true;
            yield return new WaitForSeconds(second);
            isBusy = false;
        }

        public void AnimationTrigger() => StateMachine.CurrentState.AnimationFinishTrigger();

        private void CheckForDashInput()
        {
            if (IsWallDetected()) return;
            dashUsageTimer -= Time.deltaTime;
            if (Input.GetKeyDown(KeyCode.LeftShift) && dashUsageTimer < 0f)
            {
                dashUsageTimer = dashCooldown;
                dashDir = Input.GetAxisRaw("Horizontal");

                if (dashDir == 0) dashDir = facingDir;

                StateMachine.CurrentState = PlayerDashState;
            }
        }
    }
}