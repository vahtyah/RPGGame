using UnityEngine;

namespace Enemy.Skeleton
{
    public class SkeletonAttackState : EnemyState
    {
        private EnemySkeleton enemySkeleton;

        public SkeletonAttackState(EnemyStateMachine stateMachine, Enemy enemyBase, string animBoolName,
            EnemySkeleton enemySkeleton) : base(stateMachine, enemyBase, animBoolName)
        {
            this.enemySkeleton = enemySkeleton;
        }

        public override void Enter() { base.Enter(); }

        public override void Update()
        {
            base.Update();
            enemySkeleton.SetZeroVelocity();

            if (triggerCalled)
                stateMachine.State = enemySkeleton.battleState;
        }

        public override void Exit()
        {
            base.Exit();
            enemySkeleton.lastTimeAttacked = Time.time;
        }
    }
}