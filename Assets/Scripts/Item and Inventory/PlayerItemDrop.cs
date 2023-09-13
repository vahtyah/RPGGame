using Item_and_Inventory.Test;
using UI;
using UnityEngine;

namespace Item_and_Inventory
{
    public class PlayerItemDrop : ItemDrop
    {
        [Header("Player's drop")]
        [SerializeField]
        private float chanceToLoseEquipmentItems;

        [SerializeField] private float chanceToLoseMaterialItems;

        public override void GenerateDrop()
        {
            var inventory = InventoryManager.Instance;
            var currentEquipment = inventory.equipmentInventory.inventoryItems;
            for (var i = 0; i < currentEquipment.Count;)
            {
                var item = currentEquipment[i];
                if (Random.Range(0, 100) <= chanceToLoseEquipmentItems)
                {
                    DropItem(item.itemData);
                    inventory.equipmentInventory.UnequipItem(item.itemData as EquipmentItemData,
                        item.itemSlotUI as EquipmentSlotUI);
                    inventory.backpackInventory.RemoveItem(item.itemData);
                    currentEquipment = inventory.equipmentInventory.inventoryItems;
                    continue;
                }
                i++;
            }

            var currentMaterials = inventory.backpackInventory.inventoryItems;
            for (var i = 0; i < currentMaterials.Count;)
            {
                var item = currentMaterials[i];
                if (Random.Range(0, 100) <= chanceToLoseMaterialItems)
                {
                    DropItem(item.itemData);
                    inventory.backpackInventory.RemoveItem(item.itemData);
                    currentMaterials = inventory.backpackInventory.inventoryItems;
                    continue;
                }
                i++;
            }
        }
    }
}