﻿using System;
using Item_and_Inventory;
using Item_and_Inventory.Test;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    public class PouchSlotUI : ItemSlotUI
    {
        [SerializeField] private KeyCode keyCode;
        [SerializeField] private GameObject consumableSlotPrefab;
        [SerializeField] private Transform consumableSlotParent;

        private float itemTimer;
        private PouchCooldownUI pouchCooldownUI;
        private PouchItemData pouchItemData;

        protected override void Start()
        {
            pouchCooldownUI = GetComponent<PouchCooldownUI>();
            base.Start();
        }

        public override void Setup(Item item, Inventory inventory)
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

                var newConsumableSlotUI = Instantiate(consumableSlotPrefab, consumableSlotParent)
                    .GetComponent<ConsumableSlotUI>();
                newConsumableSlotUI.Setup(new Item(pouchItemData),null);
                if (!inventory.itemDictionary.ContainsKey(pouchItemData))
                {
                    Dismantle();
                    pouchCooldownUI.Dismantle();
                    pouchItemData = null;
                }
            }
        }

        public override void OnPointerDown(PointerEventData eventData) { }
    }
}