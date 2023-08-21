using System;
using Item_and_Inventory;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    public class CraftSlotUI : ItemSlotUI
    {
        private void OnEnable()
        {
            // Setup(item);
        }

        public override void OnPointerDown(PointerEventData eventData)
        {
            var craftData = item.itemData as ItemDataEquipment;
            Inventory.Instance.CanCraft(craftData, craftData!.craftingMaterials);
        }
    }
}