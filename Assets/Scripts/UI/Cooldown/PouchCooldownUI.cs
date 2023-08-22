using Item_and_Inventory;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class PouchCooldownUI : CooldownUI
    {
        [SerializeField] private Sprite defaultImage;
        private Image image;
        public PouchItemData data { get; private set; }
        private float itemTimer;
        private float itemCooldown;

        protected override void Start() { image = GetComponent<Image>(); }

        protected override void Update()
        {
            if (data)
            {
                itemTimer -= Time.deltaTime;
                base.Update();
            }
        }

        public void Setup(PouchItemData data, float itemCooldown)
        {
            this.data = data;
            cooldownImage.sprite = this.data.itemIcon;
            this.itemCooldown = itemCooldown;
        }

        public void Dismantle()
        {
            image.sprite = cooldownImage.sprite = defaultImage;
            cooldownImage.fillAmount = 0;
            cooldownText.text = "";
            data = null;
        }

        protected override void UpdateCooldownOf()
        {
            if (cooldownImage.fillAmount > 0)
            {
                cooldownImage.fillAmount = CooldownNormalized();
                cooldownText.text = itemTimer.ToString("F1");
            }
            else
            {
                base.UpdateCooldownOf();
            }
        }

        public override void SetCooldownOf()
        {
            base.SetCooldownOf();
            itemTimer = itemCooldown;
        }

        private float CooldownNormalized() { return itemTimer / data.itemCooldown; }
    }
}