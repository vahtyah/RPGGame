using UnityEngine;

namespace Player
{
    public class PlayerWallSlideState : PlayerState
    {
        public PlayerWallSlideState(PlayerStateMachine playerStateMachine, Player player, string animBoolName) : base(
            playerStateMachine, player, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
        }

        public override void Update()
        {
            base.Update();
            if (Input.GetKeyDown(KeyCode.Space))
            {
                playerStateMachine.CurrentState = player.PlayerWallJumpState;
                return;
            }

            if (yInput < 0)
                player.SetVelocity(0, rigidbody2D.velocity.y);
            else
                player.SetVelocity(0, rigidbody2D.velocity.y * .7f);
        
            if ((xInput != 0 && player.facingDir != xInput) || player.IsGroundDetected() )
                playerStateMachine.CurrentState = player.PlayerIdleState;

            if (!player.IsWallDetected())
                playerStateMachine.CurrentState = player.PlayerAirState;
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}