using System;
using Item_and_Inventory;
using Item_and_Inventory.Test;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    public class CraftSlotUI : ItemSlotUI
    {
       public override void OnPointerDown(PointerEventData eventData)
        {
            var craftData = item.itemData as EquipmentItemData;
            // InventoryManager.Instance.craftInventory.CanCraft(craftData, craftData!.craftingMaterials);
            InventoryManager.Instance.craftInventory.ShowCraftInfo(craftData,craftData!.craftingMaterials);
        }
    }
}