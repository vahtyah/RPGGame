using UnityEngine;

namespace Skill.Sword
{
    public class SwordSkillTypeMachine
    {
        private SwordSkillType currentType;

        public SwordSkillType CurrentType
        {
            get => currentType;
            set
            {
                currentType?.Exit();
                currentType = value;
                currentType?.Setup();
            }
        }
    }
}