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
        private bool isUpdateHealth;
        private float healthNormalized;

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

        private void Update()
        {
            if (isUpdateHealth)
            {
                var preScale = rectFillHp.transform.localScale;
                rectFillHp.transform.localScale = Vector3.Lerp(rectFillHp.transform.localScale,
                    new Vector3(healthNormalized, rectFillHp.transform.localScale.y),
                    2 * Time.deltaTime);
                if (preScale == rectFillHp.transform.localScale)
                {
                    isUpdateHealth = false;
                }
            }
        }

        private void UpdateHeathBar()
        {
            healthNormalized = Mathf.Clamp(playerStats.GetHealthAmountNormalized, 0, 1);
            isUpdateHealth = true;
        }
    }
}