using System;
using Player;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class InGameUI : MonoBehaviour
    {
        [SerializeField] private RectTransform rectFillHp;
        [SerializeField] private TextMeshProUGUI currencyText;
        private PlayerStats playerStats;

        private void Start()
        {
            playerStats = PlayerManager.Instance.player.GetComponent<PlayerStats>();
            
            //Event
            PlayerManager.Instance.onCurrencyChanged += (sender, currency) =>
            {
                currencyText.text = currency.ToString("C0");
            };
            playerStats.onHealthChanged +=
                delegate(object sender, EventArgs args) { UpdateHeathBar(); };
        }

        private void UpdateHeathBar()
        {
            rectFillHp.transform.localScale = new Vector3(playerStats.GetHealthAmountNormalized, 1, 1);
        }
    }
}