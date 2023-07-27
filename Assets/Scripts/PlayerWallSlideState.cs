using Unity.VisualScripting;
using UnityEngine;

public class PlayerWallSlideState : PlayerState
{
    public PlayerWallSlideState(PlayerStateMachine playerStateMachine, Player player, string animBoolName) : base(
        playerStateMachine, player, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _playerStateMachine.CurrentState = _player.PlayerWallJumpState;
            return;
        }

        if (yInput < 0)
            _player.SetVelocity(0, _rigidbody2D.velocity.y);
        else
            _player.SetVelocity(0, _rigidbody2D.velocity.y * .7f);
        
        if ((xInput != 0 && _player.transform.localScale.x != xInput) || _player.IsGroundDetected() )
            _playerStateMachine.CurrentState = _player.PlayerIdleState;

        if (!_player.IsWallDetected())
            _playerStateMachine.CurrentState = _player.PlayerAirState;
    }

    public override void Exit()
    {
        base.Exit();
    }
}