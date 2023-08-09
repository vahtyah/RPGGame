using UnityEngine;

namespace Skill.Crystal
{
    public class CrystalSkillTypeMachine
    {
        private CrystalSkillType currentType;

        public CrystalSkillType CurrentType
        {
            get => currentType;
            set
            {
                currentType = value;
                currentType?.Setup();
            }
        }
    }
}