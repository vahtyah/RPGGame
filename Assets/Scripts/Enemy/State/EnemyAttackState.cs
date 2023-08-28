using UnityEngine;

namespace Enemy.State
{
    public class EnemyAttackState : EnemyState
    {
        public EnemyAttackState(EnemyStateMachine stateMachine, Enemy enemyBase, string animBoolName) : base(
            stateMachine, enemyBase, animBoolName)
        {
        }
        
        public override void Update()
        {
            base.Update();
            enemyBase.SetZeroVelocity();

            if (triggerCalled)
                stateMachine.State = enemyBase.battleState;
        }

        public override void Exit()
        {
            base.Exit();
            enemyBase.lastTimeAttacked = Time.time;
        }
    }
}