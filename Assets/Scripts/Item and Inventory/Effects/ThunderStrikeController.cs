using System;
using Player;
using UnityEngine;

namespace Item_and_Inventory
{
    public class ThunderStrikeController : MonoBehaviour
    {
        protected PlayerStats playerStats;

        protected virtual void Start()
        {
            playerStats = PlayerManager.Instance.player.GetComponent<PlayerStats>();
        }

        protected virtual void OnTriggerEnter2D(Collider2D other)
        {
            if (other.GetComponent<Enemy.Enemy>() != null)
            {
                var enemyTarget = other.GetComponent<EnemyStats>();
                playerStats.DoMagicDamage(enemyTarget);
            }
        }
    }
}