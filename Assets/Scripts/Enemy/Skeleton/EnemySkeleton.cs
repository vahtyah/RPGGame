using UnityEngine;

namespace Enemy.Skeleton
{
    public class EnemySkeleton : Enemy
    {
        #region States

        public SkeletonIdleState idleState { get; private set; }
        public SkeletonMoveState moveState { get; private set; }
        public SkeletonBattleState battleState { get; private set; }
        public SkeletonAttackState attackState { get; private set; }
        public SkeletonStunnedState stunnedState { get; private set; }

        #endregion

        protected override void Awake()
        {
            base.Awake();
            idleState = new SkeletonIdleState(stateMachine, this, "Idle", this);
            moveState = new SkeletonMoveState(stateMachine, this, "Move", this);
            battleState = new SkeletonBattleState(stateMachine, this, "Move", this);
            attackState = new SkeletonAttackState(stateMachine, this, "Attack", this);
            stunnedState = new SkeletonStunnedState(stateMachine, this, "Stunned", this);
        }

        protected override void Start()
        {
            base.Start();
            stateMachine.State = idleState;
        }

        protected override void Update()
        {
            base.Update();
        }

        public override bool CanBeStunned()
        {
            if (!base.CanBeStunned()) return false;
            stateMachine.State = stunnedState;
            return true;
        }
    }
}