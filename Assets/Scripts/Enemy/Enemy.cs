using System;
using System.Collections;
using Enemy.Skeleton;
using Enemy.State;
using UnityEngine;

namespace Enemy
{
    public class Enemy : Entity
    {
        [SerializeField] protected LayerMask whatIsPlayer;

        [Header("Move Info")]
        public float moveSpeed;

        public float idleTime;
        public float battleTime;
        private float defaultMoveSpeed;

        [Header("Attack Info")]
        public float attackDistance;

        public float attackCooldown;
        public float lastTimeAttacked;

        [Header("Stunned Info")]
        public float stunDuration;

        public Vector2 stunDirection;
        protected bool canBeStunned;
        [SerializeField] private GameObject counterImage;

        public string lastAnimBoolName { get; private set; }

        #region State

        public EnemyStateMachine stateMachine { get; private set; }

        public EnemyAirState airState { get; protected set; }
        public EnemyIdleState idleState { get; protected set; }
        public EnemyMoveState moveState { get; protected set; }
        public EnemyBattleState battleState { get; protected set; }
        public EnemyAttackState attackState { get; protected set; }

        #endregion

        protected override void Awake()
        {
            base.Awake();
            stateMachine = new EnemyStateMachine();
            defaultMoveSpeed = moveSpeed;

            airState = new EnemyAirState(stateMachine, this, "Idle");
            idleState = new EnemyIdleState(stateMachine, this, "Idle");
            moveState = new EnemyMoveState(stateMachine, this, "Move");
            battleState = new EnemyBattleState(stateMachine, this, "Move");
            attackState = new EnemyAttackState(stateMachine, this, "Attack");
        }

        protected override void Start()
        {
            base.Start();
            stateMachine.State = idleState;
        }

        protected override void Update()
        {
            base.Update();
            stateMachine.State.Update();
        }

        public override void SlowEntityBy(float slowPercentage, float slowDuration)
        {
            base.SlowEntityBy(slowPercentage, slowDuration);
            moveSpeed *= (1 - slowPercentage);
            anim.speed *= (1 - slowPercentage);

            Invoke("ReturnDefaultSpeed", slowDuration);
        }

        public override void ReturnDefaultSpeed()
        {
            base.ReturnDefaultSpeed();
            moveSpeed = defaultMoveSpeed;
        }

        public virtual void AssignLastAnimBoolName(string lastAnimBoolName) => this.lastAnimBoolName = lastAnimBoolName;

        public virtual void FreezeTimer(bool timeFroze)
        {
            if (timeFroze)
            {
                moveSpeed = 0;
                anim.speed = 0;
            }
            else
            {
                moveSpeed = defaultMoveSpeed;
                anim.speed = 1;
            }
        }

        public void FreezeTimerFor(float second) { StartCoroutine(FreezeTimerCoroutine(second)); }

        protected virtual IEnumerator FreezeTimerCoroutine(float second)
        {
            FreezeTimer(true);
            yield return new WaitForSeconds(second);
            FreezeTimer(false);
        }

        public virtual void OpenCounterAttackWindow()
        {
            canBeStunned = true;
            counterImage.SetActive(true);
        }

        public virtual void CloseCounterAttackWindow()
        {
            canBeStunned = false;
            counterImage.SetActive(false);
        }

        public virtual bool CanBeStunned()
        {
            if (!canBeStunned) return false;
            CloseCounterAttackWindow();
            return true;
        }

        public virtual void AnimationFinishTrigger() => stateMachine.State.AnimationFinishTrigger();

        public virtual RaycastHit2D IsPlayerDetected() =>
            Physics2D.Raycast(wallCheck.position, Vector2.right * facingDir, 50, whatIsPlayer);

        protected override void OnDrawGizmos()
        {
            base.OnDrawGizmos();
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(transform.position,
                new Vector3(transform.position.x + attackDistance * facingDir, transform.position.y));
        }
    }
}