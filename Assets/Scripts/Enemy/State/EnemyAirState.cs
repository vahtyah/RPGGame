
using UnityEngine;

namespace Enemy
{
    public class EnemyAirState : EnemyState
    {
        private float flyTime = .2f;
        private float defaultGravity;

        public EnemyAirState(EnemyStateMachine stateMachine, Enemy enemyBase, string animBoolName) : base(stateMachine,
            enemyBase, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            defaultGravity = rb.gravityScale;
            rb.gravityScale = 0;
            enemyBase.anim.speed = 0;
            stateTimer = flyTime;
        }
        
        public override void Update()
        {
            base.Update();
            if (stateTimer > 0)
            {
                rb.velocity = new Vector2(0, 20f);
            }
            else
            {
                rb.velocity = new Vector2(0, -.1f);
            }
        }
        
        public override void Exit()
        {
            base.Exit();
            rb.gravityScale = defaultGravity;
            enemyBase.anim.speed = 1;
        }
    }
}