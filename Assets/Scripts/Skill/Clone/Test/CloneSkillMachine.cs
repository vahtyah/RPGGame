namespace Skill.Test
{
    public class CloneSkillMachine
    {
        private CloneSkillType typeSkill;

        public CloneSkillType SkillType
        {
            get => typeSkill;
            set
            {
                typeSkill = value;
                typeSkill?.Setup();
            }
        }
    }
}