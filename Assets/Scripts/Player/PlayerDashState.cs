namespace Player
{
    public class PlayerDashState : PlayerState
    {
        public PlayerDashState(PlayerStateMachine playerStateMachine, Player player, string animBoolName) : base(
            playerStateMachine, player, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            timerState = player.dashDuration;
        }

        public override void Update()
        {
            base.Update();
            player.SetVelocity(player.dashSpeed * player.dashDir, 0);
            if (timerState < 0f)
            {
                playerStateMachine.CurrentState = player.PlayerIdleState;
            }
        }

        public override void Exit()
        {
            base.Exit();
            player.SetVelocity(0, rigidbody2D.velocity.y);
        }
    }
}