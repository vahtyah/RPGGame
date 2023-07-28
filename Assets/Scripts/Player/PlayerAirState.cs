﻿namespace Player
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
                playerStateMachine.CurrentState = player.PlayerIdleState;
            if (player.IsWallDetected())
                playerStateMachine.CurrentState = player.PlayerWallSlideState;
        
            if (xInput != 0)
            {
                player.SetVelocity(player.moveSpeed * .8f * xInput,rigidbody2D.velocity.y);
            }
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}