using UnityEngine;

namespace Enemy.Skeleton
{
    public class SkeletonMoveState : EnemyState
    {
        private EnemySkeleton enemySkeleton;

        public SkeletonMoveState(EnemyStateMachine stateMachine, Enemy enemyBase, string animBoolName, EnemySkeleton enemySkeleton) : base(stateMachine, enemyBase, animBoolName)
        {
            this.enemySkeleton = enemySkeleton;
        }

        public override void Enter()
        {
            base.Enter();
        }

        public override void Update()
        {
            base.Update();
            enemySkeleton.SetVelocity(enemySkeleton.moveSpeed * enemySkeleton.facingDir,enemySkeleton.rb.velocity.y);
            if (!enemySkeleton.IsGroundDetected() || enemySkeleton.IsWallDetected())
            {
                enemySkeleton.Flip();
                stateMachine.State = enemySkeleton.idleState;
            }
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}