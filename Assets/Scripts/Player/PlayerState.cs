using UnityEngine;

namespace Player
{
    public class PlayerState
    {
        protected readonly PlayerStateMachine stateMachine;
        protected readonly Player player;
        private readonly string animBoolName;

        protected Rigidbody2D rb;

        protected float xInput;
        protected float yInput;
        protected float timerState;

        protected bool triggerCalled;

        public PlayerState(PlayerStateMachine stateMachine, Player player, string animBoolName)
        {
            this.stateMachine = stateMachine;
            this.player = player;
            this.animBoolName = animBoolName;
        }
    
        public virtual void Enter()
        {
            player.anim.SetBool(animBoolName,true);
            rb = player.rb;
            triggerCalled = false;
        }
        public virtual void Update()
        {
            xInput = Input.GetAxisRaw("Horizontal");
            yInput = Input.GetAxisRaw("Vertical");  
            player.anim.SetFloat("yVelocity", rb.velocity.y);
            timerState -= Time.deltaTime;
        }

        public virtual void Exit()
        {
            player.anim.SetBool(animBoolName,false);        
        }

        public virtual void AnimationFinishTrigger()
        {
            triggerCalled = true;
        }
    }
}