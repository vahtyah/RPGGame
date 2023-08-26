using UnityEngine;

namespace Enemy.Skeleton
{
    public class SkeletonAirState : EnemyState
    {
        private float flyTime = .2f;
        private EnemySkeleton enemySkeleton;
        private float defaultGravity;

        public SkeletonAirState(EnemyStateMachine stateMachine, Enemy enemyBase, string animBoolName,
            EnemySkeleton enemySkeleton) : base(stateMachine, enemyBase, animBoolName)
        {
            this.enemySkeleton = enemySkeleton;
        }

        public override void Enter()
        {
            base.Enter();
            defaultGravity = rb.gravityScale;
            rb.gravityScale = 0;
            enemySkeleton.anim.speed = 0;
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
            enemySkeleton.anim.speed = 1;
        }
    }
}