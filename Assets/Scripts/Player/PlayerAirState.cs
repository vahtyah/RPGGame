namespace Player
{
    public class PlayerAirState : PlayerState
    {
        public PlayerAirState(PlayerStateMachine playerStateMachine, Player player, string animBoolName) : base(playerStateMachine, player, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
        }

        public override void Update()
        {
            base.Update();
            if (player.IsGroundDetected())
                stateMachine.State = player.idleState;
            if (player.IsWallDetected())
                stateMachine.State = player.wallSlideState;
        
            if (xInput != 0)
            {
                player.SetVelocity(player.moveSpeed * .8f * xInput,rb.velocity.y);
            }
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}