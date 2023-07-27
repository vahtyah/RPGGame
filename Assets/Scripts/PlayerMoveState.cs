using UnityEngine;

public class PlayerMoveState : PlayerState
{
    public PlayerMoveState(PlayerStateMachine playerStateMachine, Player player, string animBoolName) : base(playerStateMachine, player, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update();
        
        _player.SetVelocity(xInput * _player.moveSpeed, _rigidbody2D.velocity.y);
        
        if (xInput == 0f)
        {
            _playerStateMachine.CurrentState = _player.PlayerIdleState;
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}