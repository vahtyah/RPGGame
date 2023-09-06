using Skill;
using UnityEngine;

namespace Player
{
    public class PlayerDashState : PlayerState
    {
        public PlayerDashState(PlayerStateMachine playerStateMachine, Player player, string animBoolName) : base(
            playerStateMachine, player, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();

            player.skill.dashSkill.DashStartEffect();
            timerState = player.dashDuration;
        }

        public override void Update()
        {
            base.Update();
            player.SetVelocity(player.dashSpeed * player.dashDir, 0);
            if (timerState < 0f)
            {
                stateMachine.State = player.idleState;
            }
        }

        public override void Exit()
        {
            base.Exit();
            player.skill.dashSkill.DashFinishEffect();
            player.SetVelocity(0, rb.velocity.y);
        }
    }
}