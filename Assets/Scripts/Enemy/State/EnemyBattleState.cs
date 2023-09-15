using Player;
using UnityEngine;

namespace Enemy.State
{
    public class EnemyBattleState : EnemyState
    {
        private Transform player;
        private float moveDir;

        public EnemyBattleState(EnemyStateMachine stateMachine, Enemy enemyBase, string animBoolName) : base(
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

            if (enemyBase.IsPlayerDetected())
            {
                stateTimer = enemyBase.battleTime;
                if (enemyBase.IsPlayerDetected().distance < enemyBase.attackDistance)
                {
                    if (CanAttack())
                        stateMachine.State = enemyBase.attackState;
                }
            }
            else
            {
                if (stateTimer < 0 || Vector2.Distance(player.transform.position, enemyBase.transform.position) > 10)
                    stateMachine.State = enemyBase.idleState;
            }

            if (player.position.x > enemyBase.transform.position.x)
                moveDir = 1;
            else if (player.position.x < enemyBase.transform.position.x)
                moveDir = -1;

            enemyBase.SetVelocity(moveDir * enemyBase.moveSpeed, rb.velocity.y);
            if (!enemyBase.IsGroundDetected() || enemyBase.IsWallDetected())
            {
                enemyBase.Flip();
                stateMachine.State = enemyBase.idleState;
            }
        }


        private bool CanAttack()
        {
            if (Time.time >= enemyBase.attackCooldown + enemyBase.lastTimeAttacked && enemyBase.IsGroundDetected())
            {
                enemyBase.lastTimeAttacked = Time.time;
                return true;
            }

            return false;
        }
    }
}