using Skill;
using Skill.Blackhole;
using UnityEngine;

namespace Player
{
    public class PlayerBlackholeState : PlayerState
    {
        private float flyTime = .4f;
        private bool skillUsed;
        private float defaultsGravity;
        public PlayerBlackholeState(PlayerStateMachine stateMachine, Player player, string animBoolName) : base(stateMachine, player, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            defaultsGravity = rb.gravityScale;
            skillUsed = false;
            timerState = flyTime;
            rb.gravityScale = 0;
        }

        public override void Update()
        {
            base.Update();
            if (timerState > 0)
            {
                rb.velocity = new Vector2(0, 15f);
            }

            if (timerState < 0)
            {
                rb.velocity = new Vector2(0, -.1f);
                if (!skillUsed)
                {
                    Blackhole.Create(skill.blackholeSkill.GetBlackholePrefab, player.transform.position);
                    skillUsed = true;
                }
            }

            if (player.skill.blackholeSkill.SkillComplete())
                stateMachine.State = player.airState;
        }

        public override void Exit()
        {
            base.Exit();
            rb.gravityScale = defaultsGravity;
            player.fx.MakeTransparent(false);
        }
    }
}