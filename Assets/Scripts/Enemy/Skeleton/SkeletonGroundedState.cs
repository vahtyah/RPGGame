using UnityEngine;

namespace Enemy.Skeleton
{
    public class SkeletonGroundedState : EnemyState
    {
        protected EnemySkeleton enemySkeleton;
        protected Transform player;

        public SkeletonGroundedState(EnemyStateMachine stateMachine, Enemy enemyBase, string animBoolName,
            EnemySkeleton enemySkeleton) : base(stateMachine, enemyBase, animBoolName)
        {
            this.enemySkeleton = enemySkeleton;
        }

        public override void Enter()
        {
            base.Enter();
            player = GameObject.Find("Player").transform;
        }

        public override void Update()
        {
            base.Update();
            if (enemySkeleton.IsPlayerDetected() || Vector2.Distance(player.position, enemySkeleton.transform.position) < 2)
                stateMachine.State = enemySkeleton.battleState;
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}