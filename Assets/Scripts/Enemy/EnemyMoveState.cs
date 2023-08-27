namespace Enemy
{
    public class EnemyMoveState : EnemyState
    {
        public EnemyMoveState(EnemyStateMachine stateMachine, Enemy enemyBase, string animBoolName) : base(stateMachine, enemyBase, animBoolName)
        {
        }
        public override void Update()
        {
            base.Update();
            enemyBase.SetVelocity(enemyBase.moveSpeed * enemyBase.facingDir, rb.velocity.y);
            if (!enemyBase.IsGroundDetected() || enemyBase.IsWallDetected())
            {
                enemyBase.Flip();
                stateMachine.State = enemyBase.idleState;
            }
        }

        
    }
}