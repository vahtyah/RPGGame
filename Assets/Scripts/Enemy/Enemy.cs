using System;
using System.Collections;
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
        
        
        public EnemyStateMachine stateMachine { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            stateMachine = new EnemyStateMachine();
            defaultMoveSpeed = moveSpeed;
        }

        protected override void Start()
        {
            base.Start();
        }

        protected override void Update()
        {
            base.Update();
            stateMachine.State.Update();
        }

        public virtual void FreezeTimer(bool timeFroze)
        {
            if (timeFroze)
            {
                moveSpeed = 0;
                animator.speed = 0;
            }
            else
            {
                moveSpeed = defaultMoveSpeed;
                animator.speed = 1;
            }
        }

        protected virtual IEnumerator FreezeTimerFor(float second)
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
            Gizmos.DrawLine(transform.position, new Vector3(transform.position.x + attackDistance * facingDir, transform.position.y));
        }
    }
}