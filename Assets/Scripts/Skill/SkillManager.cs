using Skill.Blackhole;
using Skill.Clone;
using Skill.Crystal;
using Skill.LastBreath;
using Skill.Parry;
using Skill.Sword;
using UnityEngine;

namespace Skill
{
    //TODO: Clean Input use Skill
    public class SkillManager : MonoBehaviour
    {
        public static SkillManager Instance { get; private set; }
        public DashSkill dashSkill { get; private set; }
        public CloneSkill cloneSkill { get; private set; }
        public SwordSkill swordSkill { get; private set; }
        public BlackholeSkill blackholeSkill { get; private set; }
        public CrystalSkill crystalSkill { get; private set; }
        public ParrySkill parrySkill { get; private set; }
        public LastBreathSkill lastBreathSkill { get; private set; }

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
            blackholeSkill = GetComponent<BlackholeSkill>();
            crystalSkill = GetComponent<CrystalSkill>();
            parrySkill = GetComponent<ParrySkill>();
            lastBreathSkill = GetComponent<LastBreathSkill>();
        }

        private void Update()
        {
            
        }
    }
}