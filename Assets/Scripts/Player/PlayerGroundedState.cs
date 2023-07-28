using UnityEngine;

namespace Player
{
    public class PlayerGroundedState : PlayerState
    {
        public PlayerGroundedState(PlayerStateMachine playerStateMachine, Player player, string animBoolName) : base(playerStateMachine, player, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
        }

        public override void Update()
        {
            base.Update();
            if (Input.GetKeyDown(KeyCode.Mouse0))
                playerStateMachine.CurrentState = player.PlayerPrimaryAttackState;

            if (!player.IsGroundDetected())
                playerStateMachine.CurrentState = player.PlayerAirState;
        
            if (Input.GetKeyDown(KeyCode.Space) && player.IsGroundDetected())
                playerStateMachine.CurrentState = player.PlayerJumpState;
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}