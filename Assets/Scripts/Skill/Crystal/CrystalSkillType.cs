using UnityEngine;

namespace Skill.Crystal
{
    public abstract class CrystalSkillType
    {
        protected CrystalSkillController crystal;
        protected CrystalSkillTypeMachine typeMachine;

        protected CrystalSkillType(CrystalSkillController crystal, CrystalSkillTypeMachine typeMachine)
        {
            this.crystal = crystal;
            this.typeMachine = typeMachine;
        }

        public virtual void Setup()
        {
            
        }

        public virtual void Update()
        {
            
        }
    }
}