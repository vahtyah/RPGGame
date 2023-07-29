namespace Player
{
    public class PlayerMoveState : PlayerGroundedState
    {
        public PlayerMoveState(PlayerStateMachine stateMachine, Player player, string animBoolName) : base(stateMachine, player, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
        }

        public override void Update()
        {
            base.Update();
        
        
            if (xInput == 0f || player.IsWallDetected())
            {
                stateMachine.State = player.idleState;
            }
            player.SetVelocity(xInput * player.moveSpeed, rigidbody2D.velocity.y);

        }

    

        public override void Exit()
        {
            base.Exit();
        }
    }
}