using UnityEngine;

namespace Player
{
    public class PlayerCatchSwordState : PlayerState
    {
        private Transform sword;
        public PlayerCatchSwordState(PlayerStateMachine stateMachine, Player player, string animBoolName) : base(
            stateMachine, player, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            sword = player.sword.transform;
            if(player.transform.position.x > sword.position.x && player.facingDir == 1) player.Flip();
            if(player.transform.position.x < sword.position.x && player.facingDir != 1) player.Flip(); 
            rigidbody2D.velocity = new Vector2(player.swordReturnImpact * -player.facingDir, rigidbody2D.velocity.y);

        }

        public override void Update()
        {
            base.Update();
            if (triggerCalled)
                stateMachine.State = player.idleState;
        }

        public override void Exit()
        {
            base.Exit();
            player.StartCoroutine("BusyFor", .1f);
        }
    }
}