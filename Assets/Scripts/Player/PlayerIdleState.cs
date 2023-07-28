namespace Player
{
    public class PlayerIdleState : PlayerGroundedState
    {
        public PlayerIdleState(PlayerStateMachine playerStateMachine, Player player, string animBoolName) : base(playerStateMachine, player, animBoolName)
        {
        }
    
        public override void Enter()
        {
            base.Enter();
            player.ZeroVelocity();
        }

        public override void Update()
        {
            base.Update();
            if (xInput == player.transform.localScale.x && player.IsWallDetected())
                return;
            if (xInput != 0f && !player.isBusy)
                playerStateMachine.CurrentState = player.PlayerMoveState;
        }

        public override void Exit()
        {
            base.Exit();
        }

    }
}