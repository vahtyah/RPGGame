using System;
using Player;
using Skill;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class SkillCooldownUI : CooldownUI
    {
        [SerializeField] private Skill.Skill skill;

        protected override void Start()
        {
            base.Start();
            if(skill)
                skill.onSkillUsed += delegate(object sender, EventArgs args) { SetCooldownOf(); };
        }

        // public void SetSkill(Skill.Skill skill)
        // {
        //     this.skill = skill;
        //     skill.onSkillUsed += delegate(object sender, EventArgs args) { SetCooldownOf(); };
        // }

        protected override void UpdateCooldownOf()
        {
            if (cooldownImage.fillAmount > 0)
            {
                cooldownImage.fillAmount = skill.CooldownNormalized;
                cooldownText.text = skill.CooldownTimer.ToString("F1");
            }
            else
            {
                base.UpdateCooldownOf();
            }
        }
    }
}