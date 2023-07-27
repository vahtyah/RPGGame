using UnityEngine;

public class PlayerJumpState : PlayerState
{
    public PlayerJumpState(PlayerStateMachine playerStateMachine, Player player, string animBoolName) : base(playerStateMachine, player, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, _player.jumpForce);
    }

    public override void Update()
    {
        base.Update();
        if (_rigidbody2D.velocity.y < 0)
        {
            _playerStateMachine.CurrentState = _player.PlayerAirState;
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}