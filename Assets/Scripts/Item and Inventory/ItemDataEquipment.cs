using System.Collections.Generic;
using Player;
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
    
    [CreateAssetMenu(fileName = "New Item Data Equipment", menuName = "Data/Item Equipment")]
    public class ItemDataEquipment : ItemData
    {
        public EquipmentType equipmentType;

        public ItemEffect[] itemEffects;
        
        [Header("Major stats")]
        public int strength; // 1 point increase damage by 1 and crit.power by 1%
        public int agility; // 1 point increase evasion by 1% and crit.chance by 1%
        public int intelligence; // 1 point increase magic damage by 1 and magic resistance by 3
        public int vitality; // 1 point increase health by 3 or 5 points

        [Header("Offensive stats")]
        public int damage;
        public int critChance;
        public int critPower; //default 150%

        [Header("Defensive stats")]
        public int maxHealth;
        public int armor;
        public int evasion;
        public int magicResistance;

        [Header("Magic stats")]
        public int fireDamage;
        public int iceDamage;
        public int lightingDamage;

        [Header("Craft requirement")]
        public List<InventoryItem> craftingMaterials;

        public void ExecuteItemEffect(Transform targetTransform)
        {
            foreach (var item in itemEffects)
            {
                item.ExecuteEffect(targetTransform);
            }
        }
        public void AddModifiers()
        {
            var playerStats = PlayerManager.Instance.player.GetComponent<PlayerStats>();
            
            playerStats.strength.AddModifier(strength);
            playerStats.agility.AddModifier(agility);
            playerStats.intelligence.AddModifier(intelligence);
            playerStats.vitality.AddModifier(vitality);
            
            playerStats.damage.AddModifier(damage);
            playerStats.critChance.AddModifier(critChance);
            playerStats.critPower.AddModifier(critPower);
            
            playerStats.maxHealth.AddModifier(maxHealth);
            playerStats.armor.AddModifier(armor);
            playerStats.evasion.AddModifier(evasion);
            playerStats.magicResistance.AddModifier(magicResistance);
            
            playerStats.iceDamage.AddModifier(iceDamage);
            playerStats.fireDamage.AddModifier(fireDamage);
            playerStats.lightingDamage.AddModifier(lightingDamage);
        }

        public void RemoveModifiers()
        {
            var playerStats = PlayerManager.Instance.player.GetComponent<PlayerStats>();
            
            playerStats.strength.RemoveModifier(strength);
            playerStats.agility.RemoveModifier(agility);
            playerStats.intelligence.RemoveModifier(intelligence);
            playerStats.vitality.RemoveModifier(vitality);
            
            playerStats.damage.RemoveModifier(damage);
            playerStats.critChance.RemoveModifier(critChance);
            playerStats.critPower.RemoveModifier(critPower);
            
            playerStats.maxHealth.RemoveModifier(maxHealth);
            playerStats.armor.RemoveModifier(armor);
            playerStats.evasion.RemoveModifier(evasion);
            playerStats.magicResistance.RemoveModifier(magicResistance);
            
            playerStats.iceDamage.RemoveModifier(iceDamage);
            playerStats.fireDamage.RemoveModifier(fireDamage);
            playerStats.lightingDamage.RemoveModifier(lightingDamage);
        }
    }
}