using Player;
using UnityEngine;

namespace Item_and_Inventory
{
    [CreateAssetMenu(fileName = "Freeze Enemy Effect", menuName = "Data/Item Effect/Freeze Enemy Effect")]
    public class FreezeEnemyEffect : ItemEffect
    {
        [SerializeField] private float duration;

        public override void ExecuteEffect(Transform transform)
        {
            /*TODO: FIx*/
            // var playerStats = PlayerManager.Instance.player.GetComponent<PlayerStats>();
            // if (playerStats.CurrentHealth > playerStats.MaxHealthValue * .1f)
            //     return;
            // if (!Inventory.Instance.CanUseArmor()) return;
            // var colliders = Physics2D.OverlapCircleAll(transform.position, 2);
            // foreach (var hit in colliders)
            // {
            //     hit.GetComponent<Enemy.Enemy>()?.FreezeTimerFor(duration);
            // }            
        }
    }
}