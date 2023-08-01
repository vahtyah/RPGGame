using UnityEngine;

namespace Skill.Blackhole
{
    public class BlackholeSkill : Skill
    {
        [SerializeField] private int amountOfAttack;
        [SerializeField] private float cloneCooldown;
        [SerializeField] private float blackholeDuration;
        [Space]
        [SerializeField] private GameObject blackholePrefab;
        [SerializeField] private float maxSize;
        [SerializeField] private float growSpeed;
        [SerializeField] private float shrinkSpeed;

        private BlackholeSkillController currentBlackhole;

        public override void UseSkill()
        {
            base.UseSkill();
            var newBlackhole = Instantiate(blackholePrefab, player.transform.position,Quaternion.identity);
            currentBlackhole = newBlackhole.GetComponent<BlackholeSkillController>();
            currentBlackhole.SetupBlackhole(maxSize,growSpeed,shrinkSpeed,amountOfAttack,cloneCooldown, blackholeDuration);
        }

        public bool SkillComplete()
        {
            if (!currentBlackhole) return false;
            if (currentBlackhole.playerCanExitState)
            {
                currentBlackhole = null;
                return true;
            }

            return false;
        }

        public float GetBlackholeRadius() => maxSize / 2;   
    }
}