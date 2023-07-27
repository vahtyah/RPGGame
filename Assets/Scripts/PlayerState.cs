using UnityEngine;

public class PlayerState
{
    protected PlayerStateMachine _playerStateMachine;
    protected Player _player;
    private string _animBoolName;

    protected Rigidbody2D _rigidbody2D;

    protected float xInput;
    protected float yInput;
    protected float timerState;

    protected bool triggerCalled;

    public PlayerState(PlayerStateMachine playerStateMachine, Player player, string animBoolName)
    {
        _playerStateMachine = playerStateMachine;
        _player = player;
        _animBoolName = animBoolName;
    }
    
    public virtual void Enter()
    {
        _player.Animator.SetBool(_animBoolName,true);
        _rigidbody2D = _player.rb;
        triggerCalled = false;
    }
    public virtual void Update()
    {
        xInput = Input.GetAxisRaw("Horizontal");
        yInput = Input.GetAxisRaw("Vertical");  
        _player.Animator.SetFloat("yVelocity",_rigidbody2D.velocity.y);
        timerState -= Time.deltaTime;
    }

    public virtual void Exit()
    {
        _player.Animator.SetBool(_animBoolName,false);        
    }

    public virtual void AnimationFinishTrigger()
    {
        triggerCalled = true;
    }
}