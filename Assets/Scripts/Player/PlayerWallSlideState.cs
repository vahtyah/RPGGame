using UnityEngine;

namespace Player
{
    public class PlayerWallSlideState : PlayerState
    {
        public PlayerWallSlideState(PlayerStateMachine playerStateMachine, Player player, string animBoolName) : base(
            playerStateMachine, player, animBoolName)
        {
        }
        

        public override void Update()
        {
            base.Update();
            if (Input.GetKeyDown(KeyCode.Space))
            {
                stateMachine.State = player.wallJumpState;
                return;
            }

            if (yInput < 0)
                player.SetVelocity(0, rb.velocity.y);
            else
                player.SetVelocity(0, rb.velocity.y * .7f);
        
            if ((xInput != 0 && player.facingDir != xInput) || player.IsGroundDetected() )
                stateMachine.State = player.idleState;

            if (!player.IsWallDetected())
                stateMachine.State = player.airState;
        }
    }
}