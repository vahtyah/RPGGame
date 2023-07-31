using UnityEditor.Profiling.Memory.Experimental;
using UnityEngine;

namespace Skill.Sword
{
    public abstract class SwordSkillType
    {
        protected SwordSkillTest swordSkillTest;
        protected Sword sword;
        protected Rigidbody2D rb;
        protected CircleCollider2D cd;
        protected Animator anim;
        private Player.Player player;
        private bool canRotate = true;
        public bool isReturning;
        private float returnSpeed;

        private Vector2 finalDir;

        public SwordSkillType(SwordSkillTest swordSkillTest, Sword sword)
        {
            this.swordSkillTest = swordSkillTest;
            this.sword = sword;
            rb = sword.rb;
            cd = sword.cd;
            anim = this.sword.anim;
            this.player = swordSkillTest.Player;
            returnSpeed = swordSkillTest.ReturnSpeed;
        }

        public virtual void Setup()
        {
            rb.gravityScale = swordSkillTest.swordGravity;
            rb.velocity = swordSkillTest.finalDir;
            anim.SetBool("Rotation", true);
        }

        public virtual void Update()
        {
            if (canRotate)
                sword.transform.right = rb.velocity;
            
            if (isReturning)
            {
                sword.transform.position = Vector2.MoveTowards(sword.transform.position, player.transform.position,
                    returnSpeed * Time.deltaTime);
                if (Vector2.Distance(sword.transform.position, player.transform.position) < .5f)
                    player.CatchTheSword();
            }
        }

        public virtual void SkillDamage(Enemy.Enemy enemy, float freezeTimeDuration)
        {
            enemy.Damage();
            enemy.StartCoroutine("FreezeTimerFor", freezeTimeDuration);
        }

        public virtual void StuckInto(Collider2D other)
        {
            canRotate = false;
            cd.enabled = false;
            rb.isKinematic = true;
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            anim.SetBool("Rotation", false);
            sword.transform.parent = other.transform;
        }
        public virtual void ReturnSword()
        {
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            // rb.isKinematic = false;
            sword.transform.parent = null;
            isReturning = true;
        }
        
    }
}