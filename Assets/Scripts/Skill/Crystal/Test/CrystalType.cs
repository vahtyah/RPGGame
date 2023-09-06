using Skill.Test;

namespace Skill.Crystal.Test
{
    public abstract class CrystalType
    {
        protected CrystalSkill1 crystalSkill;
        protected Crystal crystal;

        protected CrystalType(CrystalSkill1 crystalSkill, Crystal crystal)
        {
            this.crystalSkill = crystalSkill;
            this.crystal = crystal;
        }
        
        public virtual void Setup(){}
        public virtual void Update(){}
    }
}