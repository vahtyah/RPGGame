using UI;
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
            
            // GameObject.Find("Canvas").GetComponent<UI.UI>().SwitchOnEndScreen();
        }
    }
}