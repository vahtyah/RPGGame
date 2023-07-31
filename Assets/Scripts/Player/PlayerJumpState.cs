using UnityEngine;

namespace Player
{
    public class PlayerJumpState : PlayerState
    {
        public PlayerJumpState(PlayerStateMachine playerStateMachine, Player player, string animBoolName) : base(playerStateMachine, player, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            rb.velocity = new Vector2(rb.velocity.x, player.jumpForce);
        }

        public override void Update()
        {
            base.Update();
            if (rb.velocity.y < 0)
            {
                stateMachine.State = player.airState;
            }
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}