using System.Runtime.InteropServices;
using UnityEngine;

public class PlayerDashState : PlayerState
{
    public PlayerDashState(PlayerStateMachine playerStateMachine, Player player, string animBoolName) : base(
        playerStateMachine, player, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        timerState = _player.dashDuration;
    }

    public override void Update()
    {
        base.Update();
        _player.SetVelocity(_player.dashSpeed * _player.dashDir, 0);
        if (timerState < 0f)
        {
            _playerStateMachine.CurrentState = _player.PlayerIdleState;
        }
    }

    public override void Exit()
    {
        base.Exit();
        _player.SetVelocity(0, _rigidbody2D.velocity.y);
    }
}