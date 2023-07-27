using UnityEngine;

public class PlayerWallJumpState : PlayerState
{
    public PlayerWallJumpState(PlayerStateMachine playerStateMachine, Player player, string animBoolName) : base(playerStateMachine, player, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        timerState = .4f;
        _player.SetVelocity(5 * -_player.transform.localScale.x, _player.jumpForce);
    }

    public override void Update()
    {
        base.Update();
        if (timerState < 0)
            _playerStateMachine.CurrentState = _player.PlayerAirState;
    }

    public override void Exit()
    {
        base.Exit();
    }
}