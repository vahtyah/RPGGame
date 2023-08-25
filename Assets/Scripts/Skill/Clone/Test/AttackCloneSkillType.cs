using UnityEngine;

namespace Skill.Test
{
    public class AttackCloneSkillType : CloneSkillType
    {
        public AttackCloneSkillType(CloneSkill1 cloneSkill, CloneController clone, string animBoolName) : base(
            cloneSkill, clone, animBoolName)
        {
        }

        public override void Setup()
        {
            base.Setup();
            clone.anim.SetInteger("AttackNumber",Random.Range(1,4));
            FaceClosestTarget();
        }

        public override void AttackTrigger()
        {
            var colliders = Physics2D.OverlapCircleAll(clone.AttackCheck.position, clone.AttackCheckRadius);
            foreach (var hit in colliders)
            {
                if (hit.GetComponent<Enemy.Enemy>() != null)
                {
                    player.stars.DoDamage(hit.GetComponent<CharacterStats>());
                }
            }
        }

        public override void AnimationTrigger()
        {
            timerClone = -.1f;
        }
        
        private void FaceClosestTarget()
        {
            var closestTarget = cloneSkill.FindClosestEnemy(player.transform);
            if (closestTarget == null) return;
            if (clone.transform.position.x > closestTarget.position.x)
            {
                clone.transform.Rotate(0,180,0);
            }
        }
    }
}