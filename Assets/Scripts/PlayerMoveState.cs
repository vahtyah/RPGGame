using UnityEngine;

public class PlayerMoveState : PlayerGroundedState
{
    public PlayerMoveState(PlayerStateMachine playerStateMachine, Player player, string animBoolName) : base(playerStateMachine, player, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update();
        
        _player.SetVelocity(xInput * _player.moveSpeed, _rigidbody2D.velocity.y);
        
        if (xInput == 0f)
        {
            _playerStateMachine.CurrentState = _player.PlayerIdleState;
        }

        Flip();
    }

    private void Flip()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(_rigidbody2D.velocity.x) > Mathf.Epsilon;
        if (playerHasHorizontalSpeed)
        {
            _player.transform.localScale = new Vector2(Mathf.Sign(_rigidbody2D.velocity.x), 1f);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}