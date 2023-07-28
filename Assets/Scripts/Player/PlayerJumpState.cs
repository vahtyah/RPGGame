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
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, player.jumpForce);
        }

        public override void Update()
        {
            base.Update();
            if (rigidbody2D.velocity.y < 0)
            {
                playerStateMachine.CurrentState = player.PlayerAirState;
            }
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}