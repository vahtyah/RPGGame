using UnityEditor.PackageManager;
using UnityEngine;

namespace Skill.Sword
{
    public class RegularSwordSkillType : SwordSkillType
    {
        public override void Setup()
        {
            base.Setup();
        }

        public override void Update()
        {
            base.Update();
        }



        public RegularSwordSkillType(SwordSkillTest swordSkillTest, Sword sword) : base(swordSkillTest, sword)
        {
        }
    }
}