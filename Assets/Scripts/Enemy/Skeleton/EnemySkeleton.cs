namespace Enemy.Skeleton
{
    public class EnemySkeleton : Enemy
    {
        #region States

        public SkeletonIdleState idleState { get; private set; }
        public SkeletonMoveState moveState { get; private set; }

        #endregion
        protected override void Awake()
        {
            base.Awake();
            idleState = new SkeletonIdleState(stateMachine, this, "Idle", this);
            moveState = new SkeletonMoveState(stateMachine, this, "Move", this);
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
    }
}  