using System;
using Skill.Blackhole;
using Skill.Consumable;
using Skill.Crystal;
using Skill.Parry;
using Skill.Sword;
using Skill.Test;
using UnityEngine;

namespace Skill
{
    public class SkillManager : MonoBehaviour
    {
        public static SkillManager Instance { get; private set; }
        public DashSkill dashSkill { get; private set; }
        public CloneSkill cloneSkill { get; private set; }
        public CloneSkill1 cloneSkill1 { get; private set; }
        public Sword.SwordSkill swordSkill { get; private set; }
        public BlackholeSkill blackholeSkill { get; private set; }
        public CrystalSkill crystalSkill { get; private set; }
        public ParrySkill parrySkill { get; private set; }
        public ConsumableSkill consumableSkill { get; private set; }

        private void Awake()
        {
            if (Instance) Destroy(gameObject);
            else Instance = this;   
        }

        private void Start()
        {
            dashSkill = GetComponent<DashSkill>();
            cloneSkill = GetComponent<CloneSkill>();
            cloneSkill1 = GetComponent<CloneSkill1>();
            swordSkill = GetComponent<Sword.SwordSkill>();
            blackholeSkill = GetComponent<BlackholeSkill>();
            crystalSkill = GetComponent<CrystalSkill>();
            parrySkill = GetComponent<ParrySkill>();
            consumableSkill = GetComponent<ConsumableSkill>();
        }

        private void Update()
        {
            
        }
    }
}