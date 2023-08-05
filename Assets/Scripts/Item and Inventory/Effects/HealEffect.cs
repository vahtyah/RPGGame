using Player;
using UnityEngine;

namespace Item_and_Inventory
{
    [CreateAssetMenu(fileName = "Heal Effect", menuName = "Data/Item Effect/Heal Effect")]
    public class HealEffect : ItemEffect
    {
        [Range(0f,1f)]
        [SerializeField] private float healPercent;
        public override void ExecuteEffect(Transform targetTransform)
        {
            var playerStats = PlayerManager.Instance.player.GetComponent<PlayerStats>();

            var healAmount = Mathf.RoundToInt(playerStats.MaxHealthValue * healPercent);
            playerStats.IncreaseHealthBy(healAmount);
        }
    }
}