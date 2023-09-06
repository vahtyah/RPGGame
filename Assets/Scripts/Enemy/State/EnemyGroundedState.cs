using Player;
using UnityEngine;

namespace Enemy.State
{
    public class EnemyGroundedState : EnemyState
    {
        private Transform player;
        public EnemyGroundedState(EnemyStateMachine stateMachine, Enemy enemyBase, string animBoolName) : base(
            stateMachine, enemyBase, animBoolName)
        {
        }
        
        public override void Enter()
        {
            base.Enter();
            player = PlayerManager.Instance.player.transform;
        }

        public override void Update()
        {
            base.Update();
            if (enemyBase.IsPlayerDetected() || Vector2.Distance(player.position, enemyBase.transform.position) < 2)
                stateMachine.State = enemyBase.battleState;
        }
    }
}