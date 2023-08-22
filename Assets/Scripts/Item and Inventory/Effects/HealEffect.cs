using System.Collections;
using Player;
using UnityEngine;

namespace Item_and_Inventory
{
    [CreateAssetMenu(fileName = "Heal Effect", menuName = "Data/Item Effect/Heal Effect")]
    public class HealEffect : ItemEffect
    {
        [SerializeField] private int amountHealth;
        [SerializeField] private int timeToIncrease;
        public override void ExecuteEffect(Transform transform)
        {
            var playerStats = PlayerManager.Instance.player.GetComponent<PlayerStats>();
            playerStats.IncreaseHealthFor1S(amountHealth,timeToIncrease);
        }
    }
}