using System;
using System.Collections.Generic;
using Player;
using Skill.Test;
using UnityEngine;
using Random = UnityEngine.Random;
using Vector2 = System.Numerics.Vector2;

namespace Skill.Blackhole
{
    public class BlackholeSkillController : MonoBehaviour
    {
        [SerializeField] private GameObject hotkeyPrefab;
        [SerializeField] private List<KeyCode> keyCodes;

        public float maxSize;
        public float growSpeed;
        private bool canGrow = true;
        private float shrinkSpeed;
        private bool canShrink;

        private bool canCreateHotkey = true;
        private bool cloneAttackReleased;
        private bool playerCanDisapear = true;
        
        public int amountOfAttack = 4;
        private float cloneAttackCooldown = .3f;
        private float cloneAttackTimer;

        private float blackholeTimer;

        public List<Transform> targets = new List<Transform>();
        private List<GameObject> createHotkey = new List<GameObject>();

        public bool playerCanExitState { get; private set; }

        public void SetupBlackhole(float maxSize, float growSpeed, float shrinkSpeed, int amountOfAttack,
            float cloneAttackCooldown, float blackholeDuration)
        {
            this.maxSize = maxSize;
            this.growSpeed = growSpeed;
            this.shrinkSpeed = shrinkSpeed;
            this.amountOfAttack = amountOfAttack;
            this.cloneAttackCooldown = cloneAttackCooldown;
            this.blackholeTimer = blackholeDuration;

            if (SkillManager.Instance.cloneSkill.CrystalInsteadOfClone)
                playerCanDisapear = false;
        }

        private void Update()
        {
            cloneAttackTimer -= Time.deltaTime;
            blackholeTimer -= Time.deltaTime;
            if (blackholeTimer <= 0)
            {
                blackholeTimer = Mathf.Infinity;
                if (targets.Count > 0)
                    ReleaseCloneAttack();
                else FinishBlackholeAbility();
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                ReleaseCloneAttack();
            }

            CloneAttackLogic();
            //TODO Clone will attack enemy closest, not target

            if (canGrow && !canShrink)
            {
                transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(maxSize, maxSize),
                    growSpeed * Time.deltaTime);
            }

            if (canShrink)
            {
                transform.localScale =
                    Vector3.Lerp(transform.localScale, new Vector3(-1, -1), shrinkSpeed * Time.deltaTime);
                if (transform.localScale.x < 0)
                    Destroy(gameObject);
            }
        }

        private void ReleaseCloneAttack()
        {
            if(targets.Count <= 0) return;

            DestroyHotkey();
            cloneAttackReleased = true;
            canCreateHotkey = false;

            if (playerCanDisapear)
            {
                playerCanDisapear = false;
                PlayerManager.Instance.player.fx.MakeTransparent(true);
            }
        }

        private void CloneAttackLogic()
        {
            if (cloneAttackTimer < 0 && cloneAttackReleased && targets.Count > 0 && amountOfAttack > 0)
            {
                
                int randomIndex = Random.Range(0, targets.Count);
                var xOffset = Random.Range(0, 2) == 0 ? -1.5f : 1.5f;
                cloneAttackTimer = cloneAttackCooldown;

                if (SkillManager.Instance.cloneSkill.CrystalInsteadOfClone)
                {
                    SkillManager.Instance.crystalSkill.CreateCrystal();
                    SkillManager.Instance.crystalSkill.CurrentCrystalChooseRandomEnemy();
                }
                else
                {
                    Clone.Create(targets[randomIndex], CloneType.Attack, new Vector3(xOffset, 0));
                }

                amountOfAttack--;
                if (amountOfAttack <= 0)
                {
                    Invoke("FinishBlackholeAbility", 1f);
                }
            }
        }

        private void FinishBlackholeAbility()
        {
            DestroyHotkey();
            playerCanExitState = true;
            cloneAttackReleased = false;
            canShrink = true;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            var enemy = other.GetComponent<Enemy.Enemy>();
            if (enemy != null)
            {
                enemy.FreezeTimer(true);

                CreateHotkey(enemy);
            }
        }

        private void OnTriggerExit2D(Collider2D other) => other.GetComponent<Enemy.Enemy>()?.FreezeTimer(false);

        private void DestroyHotkey()
        {
            if (createHotkey.Count <= 0) return;
            foreach (var hotkey in createHotkey)
            {
                Destroy(hotkey);
            }
        }

        private void CreateHotkey(Enemy.Enemy enemy)
        {
            if (keyCodes.Count <= 0) return;
            if (!canCreateHotkey) return;
            var newHotkey = Instantiate(hotkeyPrefab, enemy.transform.position + new Vector3(0, 2),
                Quaternion.identity);
            createHotkey.Add(newHotkey);
            var chooseKey = keyCodes[Random.Range(0, keyCodes.Count)];
            keyCodes.Remove(chooseKey);

            newHotkey.GetComponent<BlackholeHotkeyController>().SetupHotkey(chooseKey, enemy, this);
        }

        public void AddEnemyToList(Transform enemyTransform) => targets.Add(enemyTransform);
    }
}