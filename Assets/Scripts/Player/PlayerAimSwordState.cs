using UnityEngine;
using UnityEngine.UIElements;

namespace Player
{
    public class PlayerAimSwordState : PlayerState
    {
        public PlayerAimSwordState(PlayerStateMachine stateMachine, Player player, string animBoolName) : base(
            stateMachine, player, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter(); 
            player.skill.swordSkill.DotsActive(true);
        }

        public override void Update()
        {
            base.Update();
            player.SetZeroVelocity();
            if (Input.GetKeyUp(KeyCode.Mouse1))
                stateMachine.State = player.idleState;

            var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if(player.transform.position.x > mousePosition.x && player.facingDir == 1) player.Flip();
            if(player.transform.position.x < mousePosition.x && player.facingDir != 1) player.Flip(); 
        }

        public override void Exit()
        {
            base.Exit();
            player.StartCoroutine("BusyFor", .2f);
        }
    }
}