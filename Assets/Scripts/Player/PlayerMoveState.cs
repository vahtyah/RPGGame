namespace Player
{
    public class PlayerMoveState : PlayerGroundedState
    {
        public PlayerMoveState(PlayerStateMachine playerStateMachine, Player player, string animBoolName) : base(playerStateMachine, player, animBoolName)
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
                playerStateMachine.CurrentState = player.PlayerIdleState;
            }
            player.SetVelocity(xInput * player.moveSpeed, rigidbody2D.velocity.y);

        }

    

        public override void Exit()
        {
            base.Exit();
        }
    }
}