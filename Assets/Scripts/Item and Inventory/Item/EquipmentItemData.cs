using System;
using System.Collections.Generic;
using Player;
using Save_and_Load;
using Unity.VisualScripting;
using UnityEngine;

namespace Item_and_Inventory
{
    public enum EquipmentType
    {
        Weapon,
        Armor,
        Amulet,
        Flask
    }
    [System.Serializable]
    public class DictionaryContainer
    {
        public StatType statType;
        public int value;
        private Stats stats;
        public void AddModifiers()
        {
            stats ??= DetermineStats();
            stats.AddModifier(value);
        }
        public void RemoveModifiers()
        {
            stats ??= DetermineStats();
            stats.RemoveModifier(value);
        }

        public Stats DetermineStats()
        {
            return statType switch
            {
                StatType.strength => PlayerManager.Instance.player.GetComponent<PlayerStats>().strength,
                StatType.agility => PlayerManager.Instance.player.GetComponent<PlayerStats>().agility,
                StatType.intelligence => PlayerManager.Instance.player.GetComponent<PlayerStats>().intelligence,
                StatType.vitality => PlayerManager.Instance.player.GetComponent<PlayerStats>().vitality,
                StatType.damage => PlayerManager.Instance.player.GetComponent<PlayerStats>().damage,
                StatType.critChance => PlayerManager.Instance.player.GetComponent<PlayerStats>().critChance,
                StatType.critPower => PlayerManager.Instance.player.GetComponent<PlayerStats>().critPower,
                StatType.maxHealth => PlayerManager.Instance.player.GetComponent<PlayerStats>().maxHealth,
                StatType.armor => PlayerManager.Instance.player.GetComponent<PlayerStats>().armor,
                StatType.evasion => PlayerManager.Instance.player.GetComponent<PlayerStats>().evasion,
                StatType.magicResistance => PlayerManager.Instance.player.GetComponent<PlayerStats>().magicResistance,
                StatType.fireDamage => PlayerManager.Instance.player.GetComponent<PlayerStats>().fireDamage,
                StatType.iceDamage => PlayerManager.Instance.player.GetComponent<PlayerStats>().iceDamage,
                StatType.lightingDamage => PlayerManager.Instance.player.GetComponent<PlayerStats>().lightingDamage,
                _ => throw new ArgumentOutOfRangeException(nameof(statType), statType, null)
            };
        }
    }
    
    [CreateAssetMenu(fileName = "New Item Data Equipment", menuName = "Data/Item Equipment")]
    public class EquipmentItemData : ItemData
    {
        public EquipmentType equipmentType;
        public ItemEffect[] itemEffects;
        public float itemCooldown;

        [SerializeField] private DictionaryContainer[] statsModifier;

        [Header("Craft requirement")]
        public List<Item> craftingMaterials;

        public void ExecuteItemEffect(Transform targetTransform)
        {
            foreach (var item in itemEffects)
            {
                item.ExecuteEffect(targetTransform);
            }
        }
        public void AddModifiers()
        {
            foreach (var container in statsModifier)
            {
                container.AddModifiers();
            }
        }

        public void RemoveModifiers()
        {
            foreach (var container in statsModifier)
            {
                container.RemoveModifiers();
            }
        }
    }
}