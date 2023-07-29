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
            player.SetZeroVelocity();
        }

        public override void Update()
        {
            base.Update();
            if (xInput == player.facingDir && player.IsWallDetected())
                return;
            if (xInput != 0f && !player.isBusy)
                stateMachine.State = player.moveState;
        }

        public override void Exit()
        {
            base.Exit();
        }

    }
}