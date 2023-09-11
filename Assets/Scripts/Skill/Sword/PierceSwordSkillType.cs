using UnityEngine;

namespace Skill.Sword
{
    public class PierceSwordSkillType : SwordSkillType
    {
        private int pierceAmount;
        public PierceSwordSkillType(SwordSkill swordSkill, Sword sword) : base(swordSkill, sword)
        {
            pierceAmount = swordSkill.PierceAmount;
        }

        public override void Setup()
        {
            base.Setup();
        }

        public override void StuckInto(Collider2D other)
        {
            if (pierceAmount > 0 && other.GetComponent<Enemy.Enemy>() != null)
            {
                pierceAmount--;
                return;
            }
            base.StuckInto(other);
        }
    }
}