﻿using Item_and_Inventory;
using Skill;
using Skill.Test;
using UnityEngine;

namespace Player
{
    public class PlayerAnimationTriggers : MonoBehaviour
    {
        private Player player;

        private void Start()
        {
            player = GetComponentInParent<Player>();
        }

        private void AnimationTrigger()
        {
            player.AnimationTrigger();
        }

        private void AttackTrigger()
        {
            var colliders = Physics2D.OverlapCircleAll(player.attackCheck.position, player.attackCheckRadius);
            foreach (var hit in colliders)
            {
                if (hit.GetComponent<Enemy.Enemy>() != null)
                {
                    var target = hit.GetComponent<EnemyStats>();
                    player.stars.DoDamage(target);
                    // Inventory.Instance.GetEquipmentByType(EquipmentType.Weapon)?.ExecuteItemEffect(target.transform);
                }
            }
        }

        private void ThrowSword()
        {
            SkillManager.Instance.swordSkill.CreateSword();
        }

        private void ThrowTornado()
        {
            Tornado.Create(player.TornadoPrefab,player.GroundCheck.position, player.facingDir);
        }
    }
}