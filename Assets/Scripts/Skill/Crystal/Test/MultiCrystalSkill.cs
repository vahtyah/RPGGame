namespace Skill.Crystal.Test
{
    public class MultiCrystalSkill : CrystalType
    {
        public MultiCrystalSkill(CrystalSkill1 crystalSkill, Crystal crystal) : base(crystalSkill, crystal)
        {
        }

        public override void Setup()
        {
            base.Setup();
        }
    }
}