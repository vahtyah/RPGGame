namespace Skill.Crystal.Test
{
    public class CrystalTypeMachine
    {
        private CrystalType crystalType;

        public CrystalType CrystalType
        {
            get => crystalType;
            set
            {
                crystalType = value;
                crystalType?.Setup();
            }
        }
    }
}