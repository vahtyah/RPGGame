using UnityEngine;

public class PlayerIdleState : PlayerGroundedState
{
    public PlayerIdleState(PlayerStateMachine playerStateMachine, Player player, string animBoolName) : base(playerStateMachine, player, animBoolName)
    {
    }
    
    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update();
        if (xInput != 0f)
        {
            _playerStateMachine.CurrentState = _player.PlayerMoveState;
        }
    }

    public override void Exit()
    {
        base.Exit();
    }

}