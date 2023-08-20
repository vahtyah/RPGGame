using Item_and_Inventory;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class EquipmentCooldownUI : CooldownUI
    {
        [SerializeField] private KeyCode keyCode;
        [SerializeField] private Image image;
        [SerializeField] private TextMeshProUGUI amountText;
        public ItemDataEquipment dataEquipment { get; private set; }
        private float equipmentTimer;

        private void Start() { image = GetComponent<Image>(); }

        protected override void Update()
        {
            equipmentTimer -= Time.deltaTime;
            base.Update();
            if (Input.GetKeyDown(keyCode) && dataEquipment && equipmentTimer < 0)
            {
                dataEquipment.ExecuteItemEffect(null);
                equipmentTimer = dataEquipment.itemCooldown;
                SetCooldownOf();
            }//TODO: clean
        }

        public void Setup(InventoryItem inventoryItem)
        {
            this.dataEquipment = inventoryItem.itemData as ItemDataEquipment;
            image.sprite = this.dataEquipment!.itemIcon;
            cooldownImage.sprite = this.dataEquipment.itemIcon;
            amountText.text = inventoryItem.stackSize.ToString();
        }

        protected override void UpdateCooldownOf()
        {
            if (cooldownImage.fillAmount > 0)
            {
                cooldownImage.fillAmount = CooldownNormalized();
                cooldownText.text = equipmentTimer.ToString("F1");
            }
            base.UpdateCooldownOf();
            
        }

        private float CooldownNormalized()
        {
            return equipmentTimer / dataEquipment.itemCooldown;
        }
    }
}