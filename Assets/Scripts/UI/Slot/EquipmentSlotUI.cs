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

        public override void Setup(Item item, Inventory inventory)
        {
            base.Setup(item, inventory);
            itemImage.color = Color.white; //TODO: stupid?
        }

        public override void Dismantle()
        {
            base.Dismantle();
            inventory = null;
            itemImage.color = Color.clear;
        }

        public override void OnPointerDown(PointerEventData eventData)
        {
            if (item == null) return;
            inventoryManager.backpackInventory.AddItem(item.itemData);
            inventoryManager.equipmentInventory.UnequipItem(item.itemData as EquipmentItemData, this);
        }
    }
}