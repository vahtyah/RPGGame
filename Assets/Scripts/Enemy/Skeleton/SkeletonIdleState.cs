using UnityEngine.UIElements;

namespace Enemy.Skeleton
{
    public class SkeletonIdleState : EnemyState
    {
        private EnemySkeleton enemySkeleton;

        public SkeletonIdleState(EnemyStateMachine stateMachine, Enemy enemyBase, string animBoolName, EnemySkeleton enemySkeleton) : base(stateMachine, enemyBase, animBoolName)
        {
            this.enemySkeleton = enemySkeleton;
        }
        
        public override void Enter()
        {
            base.Enter();
            stateTimer = enemySkeleton.idleTime;
        }

        public override void Update()
        {
            base.Update();
            if (stateTimer < 0f)
                stateMachine.State = enemySkeleton.moveState;
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}