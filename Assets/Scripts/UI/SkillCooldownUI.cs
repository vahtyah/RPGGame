using System;
using Player;
using Skill;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class SkillCooldownUI : MonoBehaviour
    {
        private Image skillImage;
        [SerializeField] private TextMeshProUGUI skillCooldownText;
        [SerializeField] private Image skillCooldownImage;
        [SerializeField] private Skill.Skill skill;
        

        private void Start()
        {
            skillImage = GetComponent<Image>();
            skillCooldownText.gameObject.SetActive(false);
            if(skill)
                skill.onSkillUsed += delegate(object sender, EventArgs args) { SetCooldownOf(); };
        }

        private void Update()
        {
            UpdateCooldownOf();
        }

        // public void SetSkill(Skill.Skill skill)
        // {
        //     this.skill = skill;
        //     skill.onSkillUsed += delegate(object sender, EventArgs args) { SetCooldownOf(); };
        // }
        
        private void SetCooldownOf()
        {
            if(!skill) return;
            if (skillCooldownImage.fillAmount != 0) return;
            skillCooldownImage.fillAmount = 1;
            skillCooldownText.gameObject.SetActive(true);
        }

        private void UpdateCooldownOf()
        {
            if (skillCooldownImage.fillAmount > 0)
            {
                skillCooldownImage.fillAmount = skill.CooldownNormalized;
                skillCooldownText.text = skill.CooldownTimer.ToString("F1");
            }
            else
            {
                skillCooldownText.gameObject.SetActive(false);
                //TODO: chạy nhiều
            }
        }
    }
}