using UnityEngine;

namespace Enemy.Skeleton
{
    public class SkeletonStunnedState : EnemyState
    {
        private EnemySkeleton enemySkeleton;

        public SkeletonStunnedState(EnemyStateMachine stateMachine, Enemy enemyBase, string animBoolName,
            EnemySkeleton enemySkeleton) : base(
            stateMachine, enemyBase, animBoolName)
        {
            this.enemySkeleton = enemySkeleton;
        }

        public override void Enter()
        {
            base.Enter();
            enemySkeleton.fx.InvokeRepeating("RedColorBlink",0,.1f);
            stateTimer = enemySkeleton.stunDuration;
            enemySkeleton.rb.velocity = new Vector2(-enemySkeleton.facingDir * enemySkeleton.stunDirection.x,
                enemySkeleton.stunDirection.y);
        }

        public override void Update()
        {
            base.Update();
            if (stateTimer < 0)
                stateMachine.State = enemySkeleton.idleState;
        }

        public override void Exit()
        {
            base.Exit();
            enemySkeleton.fx.Invoke("CancelRedBlink",0);
        }
    }
}