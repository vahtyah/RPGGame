namespace Player
{
    public class PlayerWallJumpState : PlayerState
    {
        public PlayerWallJumpState(PlayerStateMachine playerStateMachine, Player player, string animBoolName) : base(playerStateMachine, player, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            timerState = .4f;
            player.SetVelocity(5 * -player.facingDir, player.jumpForce);
        }

        public override void Update()
        {
            base.Update();
            if (timerState < 0)
                playerStateMachine.CurrentState = player.PlayerAirState;
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}