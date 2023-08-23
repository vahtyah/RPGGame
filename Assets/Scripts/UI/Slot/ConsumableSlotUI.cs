using System;
using Item_and_Inventory;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    public class ConsumableSlotUI : ItemSlotUI
    {
        private TextMeshProUGUI timerText;
        private float timer;
        public override void Setup(Item item, Inventory inventory)
        {
            this.item = item;
            var pouchItem = item.itemData as PouchItemData;
            timer = pouchItem!.itemCooldown;
            timerText = GetComponentInChildren<TextMeshProUGUI>();
        }

        private void Update()
        {
            if(timerText && timer >= 0)
            {
                timer -= Time.deltaTime;
                timerText.text = Mathf.RoundToInt(timer).ToString();
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public override void OnPointerDown(PointerEventData eventData)
        {
        }
    }
}