using System;
using Item_and_Inventory;
using Player;
using TMPro;
using UnityEngine;

namespace UI
{
    public class StatSlotUI : MonoBehaviour
    {
        [SerializeField] private string statName;
        [SerializeField] private StatType statType;
        [SerializeField] private TextMeshProUGUI statNameText;
        [SerializeField] private TextMeshProUGUI statValueText;
        [SerializeField] private string statDescription;

        private void OnValidate()
        {
            gameObject.name = "Stat - " + statName;

            if (statNameText) statNameText.text = statName;
        }

        private void Start()
        {
            UpdateValueStat();
            Stats.onChanged += (sender, stats) =>
            {
                UpdateValueStat(); //TODO: run 14 times link:Stats.cs
            };
        }

        public void UpdateValueStat()
        {
            var playerStats = PlayerManager.Instance.player.GetComponent<PlayerStats>();
            if (!playerStats) return;
            statValueText.text = statType switch
            {
                StatType.maxHealth => playerStats.MaxHealthValue.ToString(),
                StatType.damage => (playerStats.damage.Value + playerStats.strength.Value).ToString(),
                StatType.critPower => (playerStats.critPower.Value + playerStats.strength.Value).ToString(),
                StatType.critChance => (playerStats.critChance.Value + playerStats.agility.Value).ToString(),
                StatType.evasion => (playerStats.evasion.Value + playerStats.agility.Value).ToString(),
                StatType.magicResistance => (playerStats.magicResistance.Value + playerStats.intelligence.Value)
                    .ToString(),
                _ => playerStats.StatOfType(statType).Value.ToString()
            };
        }
    }
}