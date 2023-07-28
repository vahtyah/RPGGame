using UnityEngine;

namespace Enemy.Skeleton
{
    public class SkeletonGroundedState : EnemyState
    {
        protected EnemySkeleton enemySkeleton;

        public SkeletonGroundedState(EnemyStateMachine stateMachine, Enemy enemyBase, string animBoolName,
            EnemySkeleton enemySkeleton) : base(stateMachine, enemyBase, animBoolName)
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
            if (enemySkeleton.IsPlayerDetected())
                stateMachine.State = enemySkeleton.battleState;
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}