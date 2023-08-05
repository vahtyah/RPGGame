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

        public override void ExecuteEffect(Transform targetTransform)
        {
            stats = PlayerManager.Instance.player.GetComponent<PlayerStats>();
            stats.IncreaseStatBy(buffAmount,buffDuration, StatToModify());
            
            //TODO: Bên ngoài đã có AddModifier rồi dmm
        }

        private Stats StatToModify()
        {
            return buffType switch
            {
                StatType.strength => stats.strength,
                StatType.agility => stats.agility,
                StatType.intelligence => stats.intelligence,
                StatType.vitality => stats.vitality,
                StatType.damage => stats.damage,
                StatType.critChance => stats.critChance,
                StatType.critPower => stats.critPower,
                StatType.maxHealth => stats.maxHealth,
                StatType.armor => stats.armor,
                StatType.evasion => stats.evasion,
                StatType.magicResistance => stats.magicResistance,
                StatType.fireDamage => stats.fireDamage,
                StatType.iceDamage => stats.iceDamage,
                StatType.lightingDamage => stats.lightingDamage,
                _ => null
            };
        }
    }
}