using System;
using UnityEngine;

public class PlayerAnimationTriggers : MonoBehaviour
{
    private Player _player;

    private void Start()
    {
        _player = GetComponentInParent<Player>();
    }

    private void AnimationTrigger()
    {
        _player.AnimationTrigger();
    }
}