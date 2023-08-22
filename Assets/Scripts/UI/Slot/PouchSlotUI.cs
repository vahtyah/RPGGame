using System;
using Item_and_Inventory;
using Item_and_Inventory.Test;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    public class PouchSlotUI : ItemSlotUI
    {
        [SerializeField] private KeyCode keyCode;

        private float itemTimer;
        private PouchCooldownUI pouchCooldownUI;
        private PouchItemData pouchItemData;

        private void Start()
        {
            pouchCooldownUI = GetComponent<PouchCooldownUI>();
        }

        public override void Setup(InventoryItem item, Inventory1 inventory)
        {
            base.Setup(item, inventory);
            pouchItemData = item.itemData as PouchItemData;
            pouchCooldownUI.Setup(pouchItemData, pouchItemData.itemCooldown);
        }

        private void Update()
        {
            itemTimer -= Time.deltaTime;
            if (Input.GetKeyDown(keyCode) && pouchItemData && itemTimer < 0)
            {
                pouchItemData.ExecuteItemEffect(null);
                itemTimer = pouchItemData.itemCooldown;
                pouchCooldownUI.SetCooldownOf();
                inventory.RemoveItem(pouchItemData);
                if (!inventory.itemDictionary.ContainsKey(pouchItemData))
                {
                    pouchCooldownUI.Dismantle();
                }
            } //TODO: clean
        }

        public override void OnPointerDown(PointerEventData eventData)
        {
            
        }
    }
}