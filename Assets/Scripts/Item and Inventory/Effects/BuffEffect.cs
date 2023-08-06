using System;
using Player;
using UnityEngine;

namespace Item_and_Inventory
{
    public enum StatType
    {
        strength,
        agility,
        intelligence,
        vitality,
        damage,
        critChance,
        critPower,
        maxHealth,
        armor,
        evasion,
        magicResistance,
        fireDamage,
        iceDamage,
        lightingDamage
    }
    [CreateAssetMenu(fileName = "Buff Effect", menuName = "Data/Item Effect/Buff Effect")]
    public class BuffEffect : ItemEffect
    {
        private PlayerStats stats;
        [SerializeField] private StatType buffType;
        [SerializeField] private int buffAmount;
        [SerializeField] private float buffDuration;

        public override void ExecuteEffect(Transform transform)
        {
            stats = PlayerManager.Instance.player.GetComponent<PlayerStats>();
            stats.IncreaseStatBy(buffAmount,buffDuration, stats.StatOfType(buffType));
            
            //TODO: Bên ngoài đã có AddModifier rồi dmm
        }
    }
}