using UnityEditor.Profiling.Memory.Experimental;
using UnityEngine;

namespace Skill.Sword
{
    public class SwordSkillType
    {
        protected SwordSkillTest swordSkillTest;
        protected Sword sword;
        protected Rigidbody2D rb;
        protected CircleCollider2D cd;
        protected Animator anim;

        private Vector2 finalDir;

        public SwordSkillType(SwordSkillTest swordSkillTest, Sword sword)
        {
            this.swordSkillTest = swordSkillTest;
            this.sword = sword;
            rb = sword.rb;
            cd = sword.cd;
            anim = this.sword.anim;
        }

        public virtual void Setup()
        {
            rb.gravityScale = swordSkillTest.swordGravity;
            rb.velocity = swordSkillTest.finalDir;
            anim.SetBool("Rotation", true);
        }

        public virtual void Update()
        {
        }

        public virtual void Exit()
        {
        }
    }
}