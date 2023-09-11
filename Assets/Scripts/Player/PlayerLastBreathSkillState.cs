using System.Collections;
using Enemy.Skeleton;
using Skill;
using Skill.Clone;
using Skill.Test;
using UnityEngine;

namespace Player
{
    public class PlayerLastBreathSkillState : PlayerState
    {
        private CloneSkill cloneSkill;
        private int defaultLayer;

        public PlayerLastBreathSkillState(PlayerStateMachine stateMachine, Player player, string animBoolName) : base(
            stateMachine, player, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            player.fx.MakeTransparent(true);
            (defaultLayer, player.gameObject.layer) = (player.gameObject.layer, 0);
        }

        public override void Update()
        {
            base.Update();
            player.SetZeroVelocity();
        }

        public override void Exit()
        {
            base.Exit();
            player.fx.MakeTransparent(false);
            player.gameObject.layer = defaultLayer;
        }
    }
}