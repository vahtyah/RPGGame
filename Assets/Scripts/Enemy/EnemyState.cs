using UnityEngine;

namespace Enemy
{
    public abstract class EnemyState
    {
        protected EnemyStateMachine stateMachine;
        protected Enemy enemy;
        private string animBoolName;
        
        private float stateTimer;
        protected bool triggerCalled;

        protected EnemyState(EnemyStateMachine stateMachine, Enemy enemy, string animBoolName)
        {
            this.stateMachine = stateMachine;
            this.animBoolName = animBoolName;
            this.enemy = enemy;
        }

        public virtual void Enter()
        {
            triggerCalled = false;
            enemy.animator.SetBool(animBoolName,true);
        }

        public virtual void Update()
        {
            stateTimer -= Time.deltaTime;
        }

        public virtual void Exit()
        {
            enemy.animator.SetBool(animBoolName,false);
        }
    }
}