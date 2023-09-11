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
                currencyText.text = currency.ToString();
            };
            playerStats.onHealthChanged +=
                delegate(object sender, EventArgs args) { UpdateHeathBar(); };
        }

        private void UpdateHeathBar()
        {
            var healthNor = Mathf.Clamp(playerStats.GetHealthAmountNormalized, 0, 1);
            rectFillHp.transform.localScale = new Vector3(healthNor, 1, 1);
        }
    }
}