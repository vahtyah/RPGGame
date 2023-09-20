using System.Collections;
using Skill;
using Skill.Clone;
using Skill.Test;
using UnityEngine;
using UnityEngine.Serialization;

namespace Player
{
    public class Player : Entity
    {
        [SerializeField] private GameObject tornadoPrefab;
        [Header("Attack details")] 
        public Vector2[] attackMovement;
        public bool isBusy { get; private set; }

        [Header("Move Info")] public float moveSpeed = 8f;
        public float jumpForce = 12f;
        public float swordReturnImpact;
        private float defaultMoveSpeed;
        private float defaultJumpForce;

        [Header("Dash Info")] public float dashCooldown = 1f;
        private float dashUsageTimer;
        public float dashSpeed = 25;
        public float dashDuration = 0.2f;
        private float defaultDashSpeed;
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
        public PlayerHoldSwordState holdSwordState { get; private set; }
        public PlayerHoldTornadoState holdTornadoState { get; private set; }
        public PlayerCatchSwordState catchSwordState { get; private set; }
        public PlayerBlackholeState blackholeState { get; private set; }
        public PlayerDeadState deadState { get; private set; }
        public PlayerLastBreathSkillState lastBreathSkillState { get; private set; }

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
            holdSwordState = new PlayerHoldSwordState(stateMachine, this, "Hold");
            holdTornadoState = new PlayerHoldTornadoState(stateMachine, this, "Hold");
            catchSwordState = new PlayerCatchSwordState(stateMachine, this, "CatchSword");
            blackholeState = new PlayerBlackholeState(stateMachine, this, "Jump");
            deadState = new PlayerDeadState(stateMachine, this, "Die");
            lastBreathSkillState = new PlayerLastBreathSkillState(stateMachine, this, "Idle");
        }

        protected override void Start()
        {
            base.Start();
            skill = SkillManager.Instance;
            stateMachine.State = idleState;
            
            defaultJumpForce = jumpForce;
            defaultMoveSpeed = moveSpeed;
            defaultDashSpeed = dashSpeed;
        }

        protected override void Update()
        {
            base.Update();
            stateMachine.State.Update();
            
            skill.UseDashSkill();
            skill.UseCrystalSkill();
            skill.UseLastBreathSkill();
        }

        public override void SlowEntityBy(float slowPercentage, float slowDuration)
        {
            base.SlowEntityBy(slowPercentage, slowDuration);
            moveSpeed *= (1 - slowPercentage);
            jumpForce *= (1 - slowPercentage);
            dashSpeed *= (1 - slowPercentage);
            anim.speed *= (1 - slowPercentage);
            
            Invoke("ReturnDefaultSpeed",slowDuration);
        }

        public override void ReturnDefaultSpeed()
        {
            base.ReturnDefaultSpeed();
            moveSpeed = defaultMoveSpeed;
            jumpForce = defaultJumpForce;
            dashSpeed = defaultDashSpeed;
        }

        public void AssignNewSword(GameObject newSword)
        {
            if(sword != null) return;
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

        public override void Die()
        {
            base.Die();
            stateMachine.State = deadState;
        }

        public GameObject TornadoPrefab => tornadoPrefab;
        public float GetFacingDirection => facingDir;
    }
}