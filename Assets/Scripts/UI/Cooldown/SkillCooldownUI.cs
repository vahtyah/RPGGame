using System;
using Player;
using Save_and_Load;
using Skill;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class SkillCooldownUI : CooldownUI, ISaveManager
    {
        [SerializeField] private Skill.Skill skill;

        protected override void Start()
        {
            base.Start();
            if (skill)
                skill.onSkillUsed += delegate(object sender, EventArgs args) { SetCooldownOf(); };
        }

        // public void SetSkill(Skill.Skill skill)
        // {
        //     this.skill = skill;
        //     skill.onSkillUsed += delegate(object sender, EventArgs args) { SetCooldownOf(); };
        // }

        public void SetImageSkill(Sprite sprite)
        {
            skillImage.sprite = sprite;
            cooldownImage.sprite = sprite;
        }

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

        public void LoadData(GameData data)
        {
            if (data.skillCooldownImg.TryGetValue(skill.GetType().ToString(), out var sprite))
            {
                SetImageSkill(sprite);
            }
            else
            {
                gameObject.SetActive(false);
            }
        }

        public void SaveData(ref GameData data)
        {
            // data.skillCooldownImg.Clear();
            if(skillImage.sprite != null)
                data.skillCooldownImg.TryAdd(skill.GetType().ToString(), skillImage.sprite);
        }
    }
}