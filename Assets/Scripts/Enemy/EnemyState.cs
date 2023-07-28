using UnityEngine;

namespace Enemy
{
    public abstract class EnemyState
    {
        protected EnemyStateMachine stateMachine;
        protected Enemy enemyBase;
        private string animBoolName;
        protected Rigidbody2D rb;
        
        protected float stateTimer;
        protected bool triggerCalled;

        protected EnemyState(EnemyStateMachine stateMachine, Enemy enemyBase, string animBoolName)
        {
            this.stateMachine = stateMachine;
            this.animBoolName = animBoolName;
            this.enemyBase = enemyBase;
        }

        public virtual void Enter()
        {
            triggerCalled = false;
            enemyBase.animator.SetBool(animBoolName,true);
            rb = enemyBase.rb;
        }

        public virtual void Update()
        {
            stateTimer -= Time.deltaTime;
        }

        public virtual void Exit()
        {
            enemyBase.animator.SetBool(animBoolName,false);
        }
    }
}