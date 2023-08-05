﻿using UnityEngine;

namespace Player
{
    public class PlayerPrimaryAttackState : PlayerState
    {
        public int comboCounter { get; private set; }
        private float lastTimeAttacked;
        private float comboWindow = 2f;

        public PlayerPrimaryAttackState(PlayerStateMachine playerStateMachine, Player player, string animBoolName) : base(
            playerStateMachine, player, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            xInput = 0; //Fix direction attack
            if (comboCounter > 2 || Time.time >= lastTimeAttacked + comboWindow)
                comboCounter = 0;

            player.anim.SetInteger("ComboCounter", comboCounter);

            float attackDir = player.facingDir;
            if (xInput != 0) attackDir = xInput;
        
            player.SetVelocity(player.attackMovement[comboCounter].x * attackDir,player.attackMovement[comboCounter].y);
        
            timerState = .15f;
        }

        public override void Update()
        {
            base.Update();

            if (timerState < 0)
                player.SetZeroVelocity();

            if (triggerCalled)
                stateMachine.State = player.idleState;
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