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

        if (_rigidbody2D.velocity.y == 0)
        {
            _playerStateMachine.CurrentState = _player.PlayerIdleState;
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}