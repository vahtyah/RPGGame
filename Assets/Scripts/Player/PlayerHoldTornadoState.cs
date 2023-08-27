using UnityEngine;

namespace Player
{
    public class PlayerHoldTornadoState : PlayerHoldState
    {
        public PlayerHoldTornadoState(PlayerStateMachine stateMachine, Player player, string animBoolName) : base(
            stateMachine, player, animBoolName)
        {
        }

        public override void Update()
        {
            base.Update();
            if (Input.GetKeyUp(KeyCode.K))
                stateMachine.State = player.idleState;
        }

        public override void Exit()
        {
            base.Exit();
            player.anim.SetTrigger("ThrowTornado");
        }
    }
}