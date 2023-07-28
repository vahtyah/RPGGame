﻿using UnityEngine;

namespace Player
{
    public class PlayerState
    {
        protected readonly PlayerStateMachine playerStateMachine;
        protected readonly Player player;
        private readonly string animBoolName;

        protected Rigidbody2D rigidbody2D;

        protected float xInput;
        protected float yInput;
        protected float timerState;

        protected bool triggerCalled;

        public PlayerState(PlayerStateMachine playerStateMachine, Player player, string animBoolName)
        {
            this.playerStateMachine = playerStateMachine;
            this.player = player;
            this.animBoolName = animBoolName;
        }
    
        public virtual void Enter()
        {
            player.animator.SetBool(animBoolName,true);
            rigidbody2D = player.rb;
            triggerCalled = false;
        }
        public virtual void Update()
        {
            xInput = Input.GetAxisRaw("Horizontal");
            yInput = Input.GetAxisRaw("Vertical");  
            player.animator.SetFloat("yVelocity",rigidbody2D.velocity.y);
            timerState -= Time.deltaTime;
        }

        public virtual void Exit()
        {
            player.animator.SetBool(animBoolName,false);        
        }

        public virtual void AnimationFinishTrigger()
        {
            triggerCalled = true;
        }
    }
}