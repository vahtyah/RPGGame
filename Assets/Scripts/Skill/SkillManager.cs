using System;
using Skill.Sword;
using UnityEngine;

namespace Skill
{
    public class SkillManager : MonoBehaviour
    {
        public static SkillManager Instance { get; private set; }
        public DashSkill dashSkill { get; private set; }
        public CloneSkill cloneSkill { get; private set; }
        public SwordSkill swordSkill { get; private set; }
        public SwordSkillTest swordSkillTest { get; private set; }

        private void Awake()
        {
            if (Instance) Destroy(gameObject);
            else Instance = this;   
        }

        private void Start()
        {
            dashSkill = GetComponent<DashSkill>();
            cloneSkill = GetComponent<CloneSkill>();
            swordSkill = GetComponent<SwordSkill>();
            swordSkillTest = GetComponent<SwordSkillTest>();
        }
    }
}