using Save_and_Load;
using UI;
using UnityEngine;

namespace Item_and_Inventory.Test
{
    public class EquipmentInventory : Inventory
    {
        private BackpackInventory backpackInventory;
        private EquipmentSlotUI[] equipmentSlots;

        protected override void Start()
        {
            backpackInventory = GetComponent<BackpackInventory>();
            equipmentSlots = slotParent.GetComponentsInChildren<EquipmentSlotUI>();
            base.Start();
        }

        public override bool AddItem(ItemData itemData)
        {
            var equipmentItem = itemData as EquipmentItemData;
            foreach (var equipmentSlotUI in equipmentSlots)
                if (equipmentSlotUI.equipmentType == equipmentItem!.equipmentType)
                {
                    var inventoryItem = new Item(equipmentItem, equipmentSlotUI);
                    equipmentSlotUI.Setup(inventoryItem, this);
                    inventoryItems.Add(inventoryItem);
                    itemDictionary.Add(itemData, inventoryItem);
                    equipmentItem.AddModifiers();
                }

            return true;
        }

        public void EquipItem(ItemData itemData)
        {
            var equipmentData = itemData as EquipmentItemData;

            Item oldItem = null;
            
            foreach (var slotUI in equipmentSlots)
            {
                if (slotUI.equipmentType == equipmentData!.equipmentType)
                {
                    if (slotUI.Item != null)
                    {
                        oldItem = slotUI.Item;
                        var oldEquipment = oldItem.itemData as EquipmentItemData;
                        UnequipItem(oldEquipment, slotUI);
                        oldEquipment!.RemoveModifiers();
                    }
                    AddItem(equipmentData);
                    backpackInventory.RemoveItem(itemData);
                }
            }

            if (oldItem != null)
                backpackInventory.AddItem(oldItem.itemData);
        }
        

        public void UnequipItem(EquipmentItemData data, EquipmentSlotUI slotUI)
        {
            if (itemDictionary.TryGetValue(data, out var inventoryItem))
            {
                itemDictionary.Remove(data);
                inventoryItems.Remove(inventoryItem);
                data.RemoveModifiers();
                slotUI.Dismantle();
            }
        }

        public override void LoadData(GameData data)
        {
            base.LoadData(data);
            foreach (var item in data.equipmentInventory)
                loadedItems.Add(item);
        }

        public override void SaveData(ref GameData data)
        {
            base.SaveData(ref data);
            data.equipmentInventory.Clear();
            foreach (var pair in itemDictionary)
                data.equipmentInventory.Add(pair.Value);
        }
    }
}