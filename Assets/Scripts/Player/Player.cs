﻿using System.Collections;
using Skill;
using UnityEngine;
using UnityEngine.Serialization;

namespace Player
{
    public class Player : Entity
    {
        [Header("Attack details")] 
        public Vector2[] attackMovement;
        public float counterAttackDuration = .2f;

        
        public bool isBusy { get; private set; }

        [Header("Move Info")] public float moveSpeed = 8f;
        public float jumpForce = 12f;
        public float swordReturnImpact;

        [Header("Dash Info")] public float dashCooldown = 1f;
        private float dashUsageTimer;
        public float dashSpeed = 25;
        public float dashDuration = 0.2f;
        public float dashDir { get; private set; }

        public GameObject sword { get; private set; }
        #region State

        public PlayerStateMachine stateMachine { get; private set; }
        public PlayerIdleState idleState { get; private set; }
        public PlayerMoveState moveState { get; private set; }
        public PlayerJumpState jumpState { get; private set; }
        public PlayerAirState airState { get; private set; }
        public PlayerWallSlideState wallSlideState { get; private set; }
        public PlayerDashState dashState { get; private set; }
        public PlayerWallJumpState wallJumpState { get; private set; }
        public PlayerPrimaryAttackState primaryAttackState { get; private set; }
        public PlayerCounterAttackState counterAttackState { get; private set; }
        public PlayerAimSwordState aimSwordState { get; private set; }
        public PlayerCatchSwordState catchSwordState { get; private set; }
        public PlayerBlackholeState blackholeState { get; private set; }

        #endregion

        public SkillManager skill { get; private set; }
        protected override void Awake()
        {
            stateMachine = new PlayerStateMachine();

            idleState = new PlayerIdleState(stateMachine, this, "Idle");
            moveState = new PlayerMoveState(stateMachine, this, "Move");
            jumpState = new PlayerJumpState(stateMachine, this, "Jump");
            airState = new PlayerAirState(stateMachine, this, "Jump");
            wallSlideState = new PlayerWallSlideState(stateMachine, this, "WallSlide");
            dashState = new PlayerDashState(stateMachine, this, "Dash");
            wallJumpState = new PlayerWallJumpState(stateMachine, this, "Jump");
            primaryAttackState = new PlayerPrimaryAttackState(stateMachine, this, "Attack");
            counterAttackState = new PlayerCounterAttackState(stateMachine, this, "CounterAttack");
            aimSwordState = new PlayerAimSwordState(stateMachine, this, "AimSword");
            catchSwordState = new PlayerCatchSwordState(stateMachine, this, "CatchSword");
            blackholeState = new PlayerBlackholeState(stateMachine, this, "Jump");
        }

        protected override void Start()
        {
            base.Start();
            skill = SkillManager.Instance;
            stateMachine.State = idleState;
        }

        protected override void Update()
        {
            base.Update();
            stateMachine.State.Update();
            CheckForDashInput();
        }

        public void AssignNewSword(GameObject newSword)
        {
            sword = newSword;
        }

        public void CatchTheSword()
        {
            stateMachine.State = catchSwordState;
            Destroy(sword);
        }

        private IEnumerator BusyFor(float second)
        {
            isBusy = true;
            yield return new WaitForSeconds(second);
            isBusy = false;
        }

        public void AnimationTrigger() => stateMachine.State.AnimationFinishTrigger();

        private void CheckForDashInput()
        {
            if (IsWallDetected()) return;
            if (Input.GetKeyDown(KeyCode.LeftShift) && skill.dashSkill.CanUseSkill())
            {
                dashDir = Input.GetAxisRaw("Horizontal");
                if (dashDir == 0) dashDir = facingDir;
                stateMachine.State = dashState;
            }
        }
    }
}