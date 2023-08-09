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

            if (Input.GetKeyDown(KeyCode.R))
                stateMachine.State = player.blackholeState;
            
            if (Input.GetKeyDown(KeyCode.Mouse1) && HasNoSword())
                stateMachine.State = player.aimSwordState;
            
            if (Input.GetKeyDown(KeyCode.Q))
                stateMachine.State = player.counterAttackState;
            
            if (Input.GetKeyDown(KeyCode.Mouse0))
                stateMachine.State = player.primaryAttackState;

            if (!player.IsGroundDetected())
                stateMachine.State = player.airState;
        
            if (Input.GetKeyDown(KeyCode.Space) && player.IsGroundDetected())
                stateMachine.State = player.jumpState;
        }

        public override void Exit()
        {
            base.Exit();
        }

        private bool HasNoSword()
        {
            if (!player.sword) return true;
            // player.sword.GetComponent<SwordSkillController>().ReturnSword();
            player.sword.GetComponent<SwordController>().typeMachine.CurrentType.ReturnSword();
            return false;
        }
    }
}