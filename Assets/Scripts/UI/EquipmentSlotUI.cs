using System;
using Item_and_Inventory;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    public class EquipmentSlotUI : ItemSlotUI
    {
        public EquipmentType equipmentType;

        private void OnValidate()
        {
            gameObject.name = "Equipment slot - " + equipmentType.ToString();
        }

        public override void Setup(InventoryItem item)
        {
            base.Setup(item);
            itemImage.color= Color.white; //TODO: stupid?
        }

        public override void OnPointerDown(PointerEventData eventData)
        {
            if (item.itemData == null) return;
            //TODO: Still not removed from the slot
            Inventory.Instance.UnequipItem(item.itemData as ItemDataEquipment);
            Inventory.Instance.AddItem(item.itemData as ItemDataEquipment);
            itemImage.color= Color.clear; //TODO: stupid?
        }
    }
}