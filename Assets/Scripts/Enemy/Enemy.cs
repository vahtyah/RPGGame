using System;
using UnityEngine;

namespace Enemy
{
    public class Enemy : Entity
    {
        [SerializeField] protected LayerMask whatIsPlayer;
        [Header("Move Info")] 
        public float moveSpeed;
        public float idleTime;

        [Header("Attack Info")] 
        public float attackDistance;
        
        
        public EnemyStateMachine stateMachine { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            stateMachine = new EnemyStateMachine();
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

        protected override void OnDrawGizmos()
        {
            base.OnDrawGizmos();
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(transform.position, new Vector3(transform.position.x + attackDistance * facingDir, transform.position.y));
        }

        public virtual RaycastHit2D IsPlayerDetected() =>
            Physics2D.Raycast(wallCheck.position, Vector2.right * facingDir, 50, whatIsPlayer);
    }
}