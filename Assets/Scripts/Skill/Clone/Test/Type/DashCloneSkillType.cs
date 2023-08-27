using UnityEngine;

namespace Skill.Test
{
    public class DashCloneSkillType : CloneSkillType
    {
        private Vector3 facingDir;

        public DashCloneSkillType(CloneSkill1 cloneSkill, CloneController clone, string animBoolName) : base(cloneSkill,
            clone, animBoolName)
        {
        }

        public override void Setup()
        {
            base.Setup();
            timerClone = cloneSkill.DashDuration;
            colorLosingSpeed = cloneSkill.ColorLosingSpeedDash;
        }

        public override void Update()
        {
            base.Update();
            clone.rb.velocity = new Vector2(cloneSkill.DashSpeed * facingDir.x, cloneSkill.DashSpeed * facingDir.y);
        }

        protected override void FaceDirection()
        {
            if (clone.target.GetComponent<Player.Player>())
            {
                facingDir.x = (cloneSkill.target.position - player.transform.position).normalized.x;
            }
            else
            {
                var positionTarget = clone.target.position;
                var positionClone = clone.transform.position;
                facingDir = (positionTarget - positionClone).normalized;
            }

            clone.transform.localScale = new Vector3(Mathf.Sign(facingDir.x), 1);
        }

        public override void OnTriggerEnter(Collider2D other)
        {
            base.OnTriggerEnter(other);
            if (clone.target.GetComponent<Enemy.Enemy>())
            {
                player.stars.DoDamage(clone.target.GetComponent<CharacterStats>());
                Slash.Create(cloneSkill.SlashPrefab, clone.target.transform.position, facingDir);
                Hit.Create(cloneSkill.HitPrefab, clone.target.transform.position, facingDir);
            }   
        }

        public override void AttackTrigger() { }

        public override void AnimationTrigger() { }
    }
}