using UnityEngine;

namespace Enemy.Skeleton
{
    public class SkeletonDeadState : EnemyState
    {
        private EnemySkeleton enemySkeleton;
        public SkeletonDeadState(EnemyStateMachine stateMachine, Enemy enemyBase, string animBoolName, EnemySkeleton enemySkeleton) : base(stateMachine, enemyBase, animBoolName)
        {
            this.enemySkeleton = enemySkeleton;
        }

        public override void Enter()
        {
            base.Enter(); 
            enemySkeleton.anim.SetBool(enemySkeleton.lastAnimBoolName,true);
            enemySkeleton.anim.speed = 0;
            enemySkeleton.cd.enabled = false;

            stateTimer = .1f;
        }

        public override void Update()
        {
            base.Update();
            if (stateTimer > 0)
            {
                rb.velocity = new Vector2(0, 10);
            }
        }

        public override void Exit() { base.Exit(); }
    }
}