using System.Collections;
using UnityEngine;

namespace Skill.Test
{
    public class DelayAttackCloneType : AttackCloneSkillType
    {
        public DelayAttackCloneType(CloneSkill1 cloneSkill, Clone clone, string animBoolName) : base(cloneSkill, clone, animBoolName)
        {
        }
        
        private IEnumerator CreateCloneWithDelay(Transform transform, Vector3 offset)
        {
            yield return new WaitForSeconds(.4f);
            // CreateClone(transform, offset);
        }

        protected override void FaceDirection()
        {
        }

        public override void AttackTrigger()
        {
        }

        public override void AnimationTrigger()
        {
        }
    }
}