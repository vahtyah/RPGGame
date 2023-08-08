using System;
using Item_and_Inventory;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Skill.Crystal
{
    public class CrystalSkillController : MonoBehaviour
    {
        private Animator anim => GetComponent<Animator>();
        private CircleCollider2D cd => GetComponent<CircleCollider2D>();
        private Transform closestEnemy;
        private Player.Player player;
        private float crystalExitTimer;

        private bool canExplode;
        private bool canMove;
        private float moveSpeed;

        private bool canGrow;
        private float growSpeed;
        [SerializeField] private LayerMask whatIsEnemy;

        public void Setup(Player.Player player,float crystalDuration, bool canExplode, bool canMove, float moveSpeed, float growSpeed,
            Transform closestEnemy)
        {
            this.player = player;
            crystalExitTimer = crystalDuration;
            this.canExplode = canExplode;
            this.canMove = canMove;
            this.moveSpeed = moveSpeed;
            this.growSpeed = growSpeed;
            this.closestEnemy = closestEnemy;
        }

        private void Update()
        {
            crystalExitTimer -= Time.deltaTime;
            if (crystalExitTimer <= 0)
            {
                LogicCrystal();
            }

            if (canMove)
            {
                if(!closestEnemy) return;
                transform.position =
                    Vector2.MoveTowards(transform.position, closestEnemy.position, moveSpeed * Time.deltaTime);
                if (Vector2.Distance(transform.position, closestEnemy.position) < 1f)
                {
                    LogicCrystal();
                    canMove = false;
                }
            }

            if (canGrow)
            {
                transform.localScale =
                    Vector3.Lerp(transform.localScale, new Vector3(3, 3), growSpeed * Time.deltaTime);
            }
        }

        public void ChooseRandomEnemy()
        {
            var radius = SkillManager.Instance.blackholeSkill.GetBlackholeRadius();
            var colliders = Physics2D.OverlapCircleAll(transform.position, radius, whatIsEnemy);
            if (colliders.Length > 0)
            {
                closestEnemy = colliders[Random.Range(0, colliders.Length)].transform;
            }
        }

        private void AnimationExplodeEven()
        {
            var colliders = Physics2D.OverlapCircleAll(transform.position, cd.radius);
            foreach (var hit in colliders)
            {
                if (hit.GetComponent<Enemy.Enemy>() != null)
                {
                    player.stars.DoMagicDamage(hit.GetComponent<CharacterStats>());
                    
                    Inventory.Instance.GetEquipmentByType(EquipmentType.Amulet)?.ExecuteItemEffect(hit.transform);//Effect
                }
            }
        }

        public void LogicCrystal()
        {
            if (canExplode)
            {
                canGrow = true;
                anim.SetTrigger("Explode");
            }
            else
                SelfDestroy();
        }

        private void SelfDestroy() { Destroy(gameObject); }
    }
}