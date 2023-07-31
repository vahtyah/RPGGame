using UnityEngine;

namespace Skill.Sword
{
    public class SwordSkillTypeMachine
    {
        private SwordSkillTypeController currentType;

        public SwordSkillTypeController CurrentType
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