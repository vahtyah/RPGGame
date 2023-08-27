using UnityEngine;

namespace Player
{
    public class PlayerAimTornadoState : PlayerAimState
    {
        public PlayerAimTornadoState(PlayerStateMachine stateMachine, Player player, string animBoolName) : base(
            stateMachine, player, animBoolName)
        {
        }

        public override void Update()
        {
            base.Update();
            if (Input.GetKeyUp(KeyCode.K))
                stateMachine.State = player.idleState;
        }
    }
}