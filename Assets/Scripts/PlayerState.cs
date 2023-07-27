using UnityEngine;

public class PlayerState
{
    protected PlayerStateMachine _playerStateMachine;
    protected Player _player;
    protected string _animBoolName;

    public PlayerState(PlayerStateMachine playerStateMachine, Player player, string animBoolName)
    {
        _playerStateMachine = playerStateMachine;
        _player = player;
        _animBoolName = animBoolName;
    }
    
    public virtual void Enter(){
        _player.Animator.SetBool(_animBoolName,true);        
    }
    public virtual void Update(){
        Debug.Log("I'm in " + _animBoolName);}

    public virtual void Exit()
    {
        _player.Animator.SetBool(_animBoolName,false);        
    }
}