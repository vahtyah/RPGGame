using UnityEngine;

namespace Enemy.Skeleton
{
    public class EnemySkeleton : Enemy
    {
        #region States

        public SkeletonStunnedState stunnedState { get; private set; }
        public SkeletonDeadState deadState { get; private set; }

        #endregion

        protected override void Awake()
        {
            base.Awake();
            stunnedState = new SkeletonStunnedState(stateMachine, this, "Stunned", this);
            deadState = new SkeletonDeadState(stateMachine, this, "Idle",this);
        }

        public override bool CanBeStunned()
        {
            if (!base.CanBeStunned()) return false;
            stateMachine.State = stunnedState;
            return true;
        }

        public override void Die()
        {
            base.Die();
            stateMachine.State = deadState;
        }
    }
}