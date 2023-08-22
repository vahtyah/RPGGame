using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

namespace Skill.Crystal
{
    public class CrystalSkill : Skill
    {
        [SerializeField] private float crystalDuration;
        [SerializeField] private GameObject crystalPrefab;
        private GameObject currentCrystal;

        [Header("Crystal mirage")]
        [SerializeField]
        private bool cloneInsteadOfCrystal;

        [Header("Explosive crystal")]
        [SerializeField]
        private bool canExplode;

        [Header("Moving crystal")]
        [SerializeField]
        private bool canMoveToEnemy;

        [SerializeField] private float growSpeed;
        [SerializeField] private float moveSpeed;

        [Header("Multi stacking crystal")]
        [SerializeField]
        private bool canUseMultiStacks;

        [SerializeField] private int amountOfStacks;
        [SerializeField] private float useTimeWindow;
        [SerializeField] private float multiStackCooldown;
        [SerializeField] private List<GameObject> crystalLeft = new List<GameObject>();

        public override void UseSkill()
        {
            base.UseSkill();

            if (CanUseMultiStacks())
                return;

            if (!currentCrystal)
            {
                CreateCrystal();
            }
            else
            {
                if (canMoveToEnemy) return;
                (player.transform.position, currentCrystal.transform.position) =
                    (currentCrystal.transform.position, player.transform.position);

                if (cloneInsteadOfCrystal)
                {
                    SkillManager.Instance.cloneSkill.CreateClone(currentCrystal.transform, Vector3.zero);
                    Destroy(currentCrystal);
                }
                else currentCrystal.GetComponent<CrystalSkillController>().LogicCrystal(); //TODO check or not
            }
        }

        public void CreateCrystal()
        {
            currentCrystal = Instantiate(crystalPrefab, player.transform.position, Quaternion.identity);
            var currentCrystalScript = currentCrystal.GetComponent<CrystalSkillController>();
            currentCrystalScript.Setup(player,crystalDuration, canExplode, canMoveToEnemy, moveSpeed, growSpeed,
                FindClosestEnemy(currentCrystal.transform));
        }

        public void CurrentCrystalChooseRandomEnemy()
        {
            currentCrystal.GetComponent<CrystalSkillController>().ChooseRandomEnemy();
        }

        private bool CanUseMultiStacks()
        {
            if (!canUseMultiStacks) return false;
            if (crystalLeft.Count <= 0) return true;
            
            if (crystalLeft.Count == amountOfStacks)
            {
                Invoke("ResetAbility", useTimeWindow);
            }

            cooldown = 0;
            var crystalToSpawn = crystalLeft[^1];
            crystalLeft.Remove(crystalToSpawn);
            var newCrystal = Instantiate(crystalToSpawn, player.transform.position, quaternion.identity);
            newCrystal.GetComponent<CrystalSkillController>().Setup(player,crystalDuration, canExplode, canMoveToEnemy,
                moveSpeed, growSpeed, FindClosestEnemy(newCrystal.transform));

            if (crystalLeft.Count > 0) return true;
            cooldown = multiStackCooldown;
            RefillCrystal();

            return true;
        }

        private void RefillCrystal()
        {
            int amountOfAdd = amountOfStacks - crystalLeft.Count;
            for (int i = 0; i < amountOfAdd; i++)
            {
                crystalLeft.Add(crystalPrefab);
            }
        }

        private void ResetAbility()
        {
            if (cooldownTimer > 0) return;

            cooldownTimer = multiStackCooldown;
            RefillCrystal();
        }
    }
}