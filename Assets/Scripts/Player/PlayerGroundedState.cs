using Skill;
using Skill.Sword;
using UnityEngine;

namespace Player
{
    public class PlayerGroundedState : PlayerState
    {
        public PlayerGroundedState(PlayerStateMachine stateMachine, Player player, string animBoolName) : base(stateMachine, player, animBoolName)
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
                stateMachine.State = player.primaryAttackState;

            if (!player.IsGroundDetected())
                stateMachine.State = player.airState;
        
            if (Input.GetKeyDown(KeyCode.Space) && player.IsGroundDetected())
                stateMachine.State = player.jumpState;
        }

        protected override void UseSkill()
        {
            base.UseSkill();
            skill.UseSwordSkill();
            skill.UseBlackholeSkill();
            skill.UseParrySkill();
        }

        
    }
}