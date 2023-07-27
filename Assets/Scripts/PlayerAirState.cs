using UnityEngine;

public class PlayerAirState : PlayerState
{
    public PlayerAirState(PlayerStateMachine playerStateMachine, Player player, string animBoolName) : base(playerStateMachine, player, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update();

        if (_player.IsWallDetected())
            _playerStateMachine.CurrentState = _player.PlayerWallSlideState;
        
        if (_player.IsGroundDetected())
        {
            _playerStateMachine.CurrentState = _player.PlayerIdleState;
        }

        if (xInput != 0)
        {
            _player.SetVelocity(_player.moveSpeed * .8f * xInput,_rigidbody2D.velocity.y);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}