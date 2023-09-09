using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public abstract class CooldownUI : MonoBehaviour
    {
        
        [SerializeField] protected TextMeshProUGUI cooldownText;
        [SerializeField] protected Image cooldownImage;
        protected Image skillImage;

        private void Awake()
        {
            skillImage = GetComponent<Image>();
        }

        protected virtual void Start()
        {
            cooldownText.gameObject.SetActive(false);
        }

        protected virtual void Update()
        {
            UpdateCooldownOf();
        }



        public virtual void SetCooldownOf()
        {
            if (cooldownImage.fillAmount != 0) return;
            cooldownImage.fillAmount = 1;
            cooldownText.gameObject.SetActive(true);
        }

        protected virtual void UpdateCooldownOf()
        {
            cooldownText.gameObject.SetActive(false);
        }
    }
}