﻿using Player;
using UnityEngine;

namespace Skill.Test
{
    public abstract class CloneSkillType
    {
        protected CloneSkill1 cloneSkill;
        protected CloneController clone;
        protected Player.Player player;

        private string animBoolName;
        protected float timerClone;

        protected CloneSkillType(CloneSkill1 cloneSkill, CloneController clone, string animBoolName)
        {
            this.cloneSkill = cloneSkill;
            this.clone = clone;
            this.animBoolName = animBoolName;
            timerClone = this.cloneSkill.CloneDuration;
            player = PlayerManager.Instance.player;
        }

        public virtual void Setup()
        {
            clone.anim.SetBool(animBoolName, true);
        }

        public virtual void Update()
        {
            timerClone -= Time.deltaTime;
            if (timerClone < 0)
                clone.sr.color = new Color(1, 1, 1, clone.sr.color.a - (Time.deltaTime * .5f));
            if(clone.sr.color.a <= 0)
                clone.SelfDestroy();
        }

        public abstract void AttackTrigger();
        public abstract void AnimationTrigger();
    }
}