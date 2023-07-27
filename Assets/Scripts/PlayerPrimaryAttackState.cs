using UnityEngine;

public class PlayerPrimaryAttackState : PlayerState
{
    private int comboCounter;
    private float lastTimeAttacked;
    private float comboWindow = 2f;

    public PlayerPrimaryAttackState(PlayerStateMachine playerStateMachine, Player player, string animBoolName) : base(
        playerStateMachine, player, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        if (comboCounter > 2 || Time.time >= lastTimeAttacked + comboWindow)
            comboCounter = 0;

        _player.Animator.SetInteger("ComboCounter", comboCounter);
        
        _player.SetVelocity(_player.attackMovement[comboCounter].x,_player.attackMovement[comboCounter].y);
        
        timerState = .15f;
    }

    public override void Update()
    {
        base.Update();

        if (timerState < 0)
            _player.ZeroVelocity();

        if (triggerCalled)
            _playerStateMachine.CurrentState = _player.PlayerIdleState;
    }

    public override void Exit()
    {
        base.Exit();

        _player.StartCoroutine("BusyFor", .15f);
        
        comboCounter++;
        lastTimeAttacked = Time.time;
    }
}