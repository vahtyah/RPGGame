using UnityEngine;

public class PlayerIdleState : PlayerGroundedState
{
    public PlayerIdleState(PlayerStateMachine playerStateMachine, Player player, string animBoolName) : base(playerStateMachine, player, animBoolName)
    {
    }
    
    public override void Enter()
    {
        base.Enter();
        _player.ZeroVelocity();
    }

    public override void Update()
    {
        base.Update();
        if (xInput == _player.transform.localScale.x && _player.IsWallDetected())
            return;
        if (xInput != 0f && !_player.isBusy)
            _playerStateMachine.CurrentState = _player.PlayerMoveState;
    }

    public override void Exit()
    {
        base.Exit();
    }

}