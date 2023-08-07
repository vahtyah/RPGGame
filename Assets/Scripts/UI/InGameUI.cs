using System;
using Player;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class InGameUI : MonoBehaviour
    {
        [SerializeField] private RectTransform rectFillHp;
        private PlayerStats playerStats;
        private Skill.Skill skill;
        [SerializeField] private Image dashImage;

        private void Start()
        {
            playerStats = PlayerManager.Instance.player.GetComponent<PlayerStats>();
            playerStats.onHealthChanged += delegate(object sender, EventArgs args) { UpdateHeathBar(); };
        }

        private void UpdateHeathBar()
        {
            rectFillHp.transform.localScale = new Vector3(playerStats.GetHealthAmountNormalized, 1, 1);
        }

        
    }
}