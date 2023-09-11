using Player;
using Skill.Test;
using UnityEngine;

namespace Skill.Clone.Type
{
    public abstract class CloneSkillType
    {
        protected CloneSkill cloneSkill;
        protected Clone clone;
        protected Player.Player player;

        private string animBoolName;
        protected float timerClone; 
        protected float colorLosingSpeed;

        protected CloneSkillType(CloneSkill cloneSkill, Clone clone, string animBoolName)
        {
            this.cloneSkill = cloneSkill;
            this.clone = clone;
            this.animBoolName = animBoolName;
            player = PlayerManager.Instance.player;
        }

        public virtual void Setup()
        {
            clone.anim.SetBool(animBoolName, true);
            FaceDirection();
        }

        public virtual void Update()
        {
            timerClone -= Time.deltaTime;
            if (timerClone < 0)
                clone.sr.color = new Color(1, 1, 1, clone.sr.color.a - (Time.deltaTime * colorLosingSpeed));
            if(clone.sr.color.a <= 0)
                clone.SelfDestroy();
        }

        protected abstract void FaceDirection();

        public abstract void AttackTrigger();
        public abstract void AnimationTrigger();

        public virtual void OnTriggerEnter(Collider2D other)
        {
            
        }
    }
}