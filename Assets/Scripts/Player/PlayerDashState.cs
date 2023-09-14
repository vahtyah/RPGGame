using Skill;
using Skill.Sword;
using UnityEngine;

namespace Player
{
    public class PlayerDashState : PlayerState
    {
        private DashSkill dashSkill;
        public PlayerDashState(PlayerStateMachine playerStateMachine, Player player, string animBoolName) : base(
            playerStateMachine, player, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            dashSkill = player.skill.dashSkill;
            dashSkill.DashStartEffect();
            timerState = player.dashDuration;
        }

        public override void Update()
        {
            base.Update();
            player.SetVelocity(player.dashSpeed * dashSkill.GetFacingDirection, 0);
            if (timerState < 0f)
            {
                stateMachine.State = player.idleState;
            }
        }

        public override void Exit()
        {
            base.Exit();
            dashSkill.DashFinishEffect();
            player.SetVelocity(0, rb.velocity.y);
        }
        

    }
}