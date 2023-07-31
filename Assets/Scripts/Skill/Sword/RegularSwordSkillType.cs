﻿using UnityEditor.PackageManager;
using UnityEngine;

namespace Skill.Sword
{
    public class RegularSwordSkillType : SwordSkillTypeController
    {
        public override void Setup()
        {
            base.Setup();
            rb.gravityScale = swordSkill.RegularGravity;
            anim.SetBool("Rotation", true);
        }

        public RegularSwordSkillType(SwordSkill swordSkill, Sword sword) : base(swordSkill, sword)
        {
        }
    }
}