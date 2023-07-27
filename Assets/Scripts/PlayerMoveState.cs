using UnityEngine;

public class PlayerMoveState : PlayerGroundedState
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
        
        
        if (xInput == 0f || (xInput == _player.transform.localScale.x && _player.IsWallDetected()))
        {
            _playerStateMachine.CurrentState = _player.PlayerIdleState;
        }
        _player.SetVelocity(xInput * _player.moveSpeed, _rigidbody2D.velocity.y);

    }

    

    public override void Exit()
    {
        base.Exit();
    }
}