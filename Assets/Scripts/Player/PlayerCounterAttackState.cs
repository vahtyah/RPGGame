using UnityEngine;

namespace Player
{
    public class PlayerCounterAttackState : PlayerState
    {
        private bool canCreateClone;
        public PlayerCounterAttackState(PlayerStateMachine playerStateMachine, Player player, string animBoolName) : base(playerStateMachine, player, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            canCreateClone = true;
            timerState = player.counterAttackDuration;
            player.anim.SetBool("SuccessfulAttack",false);
        }

        public override void Update()
        {
            base.Update(); 
            player.SetZeroVelocity();
            
            var colliders = Physics2D.OverlapCircleAll(player.attackCheck.position, player.attackCheckRadius);
            foreach (var hit in colliders)
            {
                if(hit.GetComponent<Enemy.Enemy>() != null)
                    if (hit.GetComponent<Enemy.Enemy>().CanBeStunned())
                    {
                        timerState = 10;
                        player.anim.SetBool("SuccessfulAttack",true);
                        if (canCreateClone)
                        {
                            canCreateClone = false;
                            player.skill.cloneSkill.CreateCloneOnCounterAttack(hit.transform);
                        }
                    }
            }

            if (timerState < 0 || triggerCalled)
                stateMachine.State = player.idleState;
        }

        public override void Exit() { base.Exit(); }
    }
}