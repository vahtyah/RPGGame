using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    public PlayerGroundedState(PlayerStateMachine playerStateMachine, Player player, string animBoolName) : base(playerStateMachine, player, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update();
        if (Input.GetKeyDown(KeyCode.Space) && _player.IsGroundDetected())
        {
            _playerStateMachine.CurrentState = _player.PlayerJumpState;
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}