using UnityEditor.Profiling.Memory.Experimental;
using UnityEngine;

namespace Skill.Sword
{
    public abstract class SwordSkillTypeController
    {
        protected SwordSkill swordSkill;
        protected Sword sword;
        protected Rigidbody2D rb;
        private CircleCollider2D cd;
        protected Animator anim;
        protected Player.Player player;
        
        private bool canRotate = true;
        protected bool isReturning;

        private Vector2 finalDir;

        public SwordSkillTypeController(SwordSkill swordSkill, Sword sword)
        {
            this.swordSkill = swordSkill;
            this.sword = sword;
            rb = sword.rb;
            cd = sword.cd;
            anim = this.sword.anim;
            player = swordSkill.Player;
        }

        public virtual void Setup()
        {
            rb.velocity = swordSkill.FinalDir;
            rb.gravityScale = swordSkill.SwordGravity;
        }

        public virtual void Update()
        {
            if (canRotate)
                sword.transform.right = rb.velocity;
            
            if (isReturning)
            {
                sword.transform.position = Vector2.MoveTowards(sword.transform.position, player.transform.position,
                     swordSkill.ReturnSpeed * Time.deltaTime);
                if (Vector2.Distance(sword.transform.position, player.transform.position) < .5f)
                    player.CatchTheSword();
            }
        }

        public virtual void Damage(Enemy.Enemy enemy)
        {
            enemy.Damage();
            enemy.StartCoroutine("FreezeTimerFor", swordSkill.FreezeTimeDuration);
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

        public bool IsReturning => isReturning;
    }
}