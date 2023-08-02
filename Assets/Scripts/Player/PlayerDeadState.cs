using UnityEngine;

namespace Player
{
    public class PlayerDeadState : PlayerState
    {
        public PlayerDeadState(PlayerStateMachine stateMachine, Player player, string animBoolName) : base(stateMachine, player, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            player.SetZeroVelocity();
        }

        public override void Update() { base.Update(); }

        public override void Exit() { base.Exit(); }
    }
}