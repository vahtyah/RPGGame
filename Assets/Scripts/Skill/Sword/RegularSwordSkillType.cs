using UnityEngine;

namespace Skill.Sword
{
    public class RegularSwordSkillType : SwordSkillType
    {
        public override void Setup()
        {
            base.Setup();
            anim.SetBool("Rotation", true);
        }

        public RegularSwordSkillType(SwordSkill swordSkill, Sword sword) : base(swordSkill, sword)
        {
        }
    }
}