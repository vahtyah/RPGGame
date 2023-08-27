namespace Enemy.Skeleton
{
    public class EnemyIdleState : EnemyState
    {
        public EnemyIdleState(EnemyStateMachine stateMachine, Enemy enemyBase, string animBoolName) : base(stateMachine, enemyBase, animBoolName)
        {
        }
        
        public override void Enter()
        {
            base.Enter();
            stateTimer = enemyBase.idleTime;
        }

        public override void Update()
        {
            base.Update();
            if (stateTimer < 0f)
                stateMachine.State = enemyBase.moveState;
        }
    }
}