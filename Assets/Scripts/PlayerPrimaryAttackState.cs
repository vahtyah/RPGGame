using UnityEngine;

public class PlayerPrimaryAttackState : PlayerState
{
    public PlayerPrimaryAttackState(PlayerStateMachine playerStateMachine, Player player, string animBoolName) : base(playerStateMachine, player, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update();
        if (!triggerCalled)
            _playerStateMachine.CurrentState = _player.PlayerIdleState;
    }

    public override void Exit()
    {
        base.Exit();
    }
}