using UnityEngine;

namespace Enemy.Skeleton
{
    public class SkeletonBattleState : EnemyState
    {
        private Transform player;
        private EnemySkeleton enemySkeleton;
        private float moveDir;

        public SkeletonBattleState(EnemyStateMachine stateMachine, Enemy enemyBase, string animBoolName,
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

            if (enemySkeleton.IsPlayerDetected())
            {
                if (enemySkeleton.IsPlayerDetected().distance < enemySkeleton.attackDistance)
                {
                    enemySkeleton.ZeroVelocity();
                    return;
                }
            }
            
            if (player.position.x > enemySkeleton.transform.position.x)
                moveDir = 1;
            else if (player.position.x < enemySkeleton.transform.position.x)
                moveDir = -1;

            enemySkeleton.SetVelocity(moveDir * enemySkeleton.moveSpeed, rb.velocity.y);
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}