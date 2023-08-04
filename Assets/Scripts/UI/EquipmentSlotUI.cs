using System;
using Item_and_Inventory;
using UnityEngine;

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
    }
}