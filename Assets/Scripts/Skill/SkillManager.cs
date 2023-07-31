using System;
using Skill.Blackhole;
using Skill.Sword;
using UnityEngine;

namespace Skill
{
    public class SkillManager : MonoBehaviour
    {
        public static SkillManager Instance { get; private set; }
        public DashSkill dashSkill { get; private set; }
        public CloneSkill cloneSkill { get; private set; }
        public Sword.SwordSkill SwordSkill { get; private set; }
        public BlackholeSkill blackholeSkill { get; private set; }

        private void Awake()
        {
            if (Instance) Destroy(gameObject);
            else Instance = this;   
        }

        private void Start()
        {
            dashSkill = GetComponent<DashSkill>();
            cloneSkill = GetComponent<CloneSkill>();
            SwordSkill = GetComponent<Sword.SwordSkill>();
            blackholeSkill = GetComponent<BlackholeSkill>();
        }
    }
}