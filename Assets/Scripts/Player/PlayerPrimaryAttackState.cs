using UnityEngine;

namespace Player
{
    public class PlayerPrimaryAttackState : PlayerState
    {
        private int comboCounter;
        private float lastTimeAttacked;
        private float comboWindow = 2f;

        public PlayerPrimaryAttackState(PlayerStateMachine playerStateMachine, Player player, string animBoolName) : base(
            playerStateMachine, player, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            if (comboCounter > 2 || Time.time >= lastTimeAttacked + comboWindow)
                comboCounter = 0;

            player.animator.SetInteger("ComboCounter", comboCounter);

            var attackDir = player.transform.localScale.x;
            if (xInput != 0) attackDir = xInput;
        
            player.SetVelocity(player.attackMovement[comboCounter].x * attackDir,player.attackMovement[comboCounter].y);
        
            timerState = .15f;
        }

        public override void Update()
        {
            base.Update();

            if (timerState < 0)
                player.ZeroVelocity();

            if (triggerCalled)
                playerStateMachine.CurrentState = player.PlayerIdleState;
        }

        public override void Exit()
        {
            base.Exit();

            player.StartCoroutine("BusyFor", .15f);
        
            comboCounter++;
            lastTimeAttacked = Time.time;
        }
    }
}