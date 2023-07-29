using UnityEngine;

namespace Player
{
    public class PlayerAimSwordState : PlayerState
    {
        public PlayerAimSwordState(PlayerStateMachine stateMachine, Player player, string animBoolName) : base(
            stateMachine, player, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter(); 
            player.skill.swordSkill.DotsActive(true);
        }

        public override void Update()
        {
            base.Update();
            player.SetZeroVelocity();
            if (Input.GetKeyUp(KeyCode.Mouse1))
                stateMachine.State = player.idleState;
        }

        public override void Exit() { base.Exit(); }
    }
}