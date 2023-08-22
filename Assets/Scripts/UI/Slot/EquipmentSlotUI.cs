using System;
using Item_and_Inventory;
using Item_and_Inventory.Test;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    public class EquipmentSlotUI : ItemSlotUI
    {
        public EquipmentType equipmentType;

        private void OnValidate() { gameObject.name = "Equipment slot - " + equipmentType.ToString(); }

        public override void Setup(InventoryItem item, Inventory1 inventory)
        {
            base.Setup(item, inventory);
            itemImage.color = Color.white; //TODO: stupid?
        }

        public void Dismantle()
        {
            item.itemData = null;
            inventory = null;
            itemImage.color = Color.clear;
        }

        public override void OnPointerDown(PointerEventData eventData)
        {
            if (item.itemData == null) return;
            inventoryManager.generalInventory.AddItem(item.itemData);
            inventoryManager.equipmentInventory.UnequipItem(item.itemData as EquipmentItemData, this);
            Dismantle();
        }
    }
}