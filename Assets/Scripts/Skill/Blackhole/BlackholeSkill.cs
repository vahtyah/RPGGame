using System;
using UI;
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
        
        [Header("Skill Tree")] [SerializeField]
        private SkillTreeSlotUI blackHole;

        private bool blackHoleUnlocker;

        

        private Blackhole currentBlackhole;

        private void Awake()
        {
            blackHole.onUnlocked += delegate(object sender, EventArgs args) { UnlockBlackHole(); };
        }

        private void UnlockBlackHole() => blackHoleUnlocker = blackHole.unlocker;

        public override bool UseSkill()
        {
            return blackHoleUnlocker && base.UseSkill();
        }

        public override void StartSkill()
        {
            base.StartSkill();
            player.stateMachine.State = player.blackholeState;
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
        public float GetMaxSize => maxSize;
        public float GetGrowSpeed => growSpeed;
        public float GetShrinkSpeed => shrinkSpeed;
        public int GetAmountOfAttack => amountOfAttack;
        public float GetCloneCooldown => cloneCooldown;
        public float GetBlackholeDuration => blackholeDuration;
        public GameObject GetBlackholePrefab => blackholePrefab;
        
    }
}