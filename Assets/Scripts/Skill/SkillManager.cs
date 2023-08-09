using System;
using Skill.Blackhole;
using Skill.Crystal;
using Skill.Sword;
using UnityEngine;

namespace Skill
{
    public class SkillManager : MonoBehaviour
    {
        public static SkillManager Instance { get; private set; }
        public DashSkill dashSkill { get; private set; }
        public CloneSkill cloneSkill { get; private set; }
        public Sword.SwordSkill swordSkill { get; private set; }
        public BlackholeSkill blackholeSkill { get; private set; }
        public CrystalSkill crystalSkill { get; private set; }

        private void Awake()
        {
            if (Instance) Destroy(gameObject);
            else Instance = this;   
        }

        private void Start()
        {
            dashSkill = GetComponent<DashSkill>();
            cloneSkill = GetComponent<CloneSkill>();
            swordSkill = GetComponent<Sword.SwordSkill>();
            blackholeSkill = GetComponent<BlackholeSkill>();
            crystalSkill = GetComponent<CrystalSkill>();
        }
    }
}