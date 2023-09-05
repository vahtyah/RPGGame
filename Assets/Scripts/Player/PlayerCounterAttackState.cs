using UnityEngine;

namespace Player
{
    public class PlayerCounterAttackState : PlayerState
    {
        public PlayerCounterAttackState(PlayerStateMachine playerStateMachine, Player player, string animBoolName) : base(playerStateMachine, player, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            player.anim.SetBool("SuccessfulAttack",false);
        }

        public override void Update()
        {
            base.Update(); 
            player.SetZeroVelocity();
            if (triggerCalled)
                player.skill.parrySkill.CompleteSkill();
        }

        public override void Exit() { base.Exit(); }
    }
}