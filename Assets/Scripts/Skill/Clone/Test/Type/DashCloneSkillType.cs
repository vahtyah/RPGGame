using UnityEngine;

namespace Skill.Test
{
    public class DashCloneSkillType : CloneSkillType
    {
        private float facingDir;
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
            clone.rb.velocity = new Vector2(cloneSkill.DashSpeed * facingDir, 0);
        }

        protected override void FaceDirection()
        {
            facingDir = player.facingDir;
            clone.transform.localScale = new Vector3(Mathf.Sign(facingDir), 1);
        }

        public override void AttackTrigger() { }

        public override void AnimationTrigger() { }
    }
}