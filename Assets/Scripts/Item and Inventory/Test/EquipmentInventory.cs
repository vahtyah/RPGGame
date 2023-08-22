using Save_and_Load;
using UI;
using UnityEngine;

namespace Item_and_Inventory.Test
{
    public class EquipmentInventory : Inventory1
    {
        private GeneralInventory generalInventory;
        
        [Header("Equipment UI")]
        [SerializeField] private EquipmentSlotUI[] equipmentSlots;

        protected override void Start()
        {
            generalInventory = GetComponent<GeneralInventory>();
            equipmentSlots = slotParent.GetComponentsInChildren<EquipmentSlotUI>();
            base.Start();
        }

        public override void AddItem(ItemData itemData)
        {
            var equipmentItem = itemData as EquipmentItemData;
            foreach (var equipmentSlotUI in equipmentSlots)
                if (equipmentSlotUI.equipmentType == equipmentItem!.equipmentType)
                {
                    
                    var inventoryItem = new InventoryItem(equipmentItem, null, equipmentSlotUI);
                    equipmentSlotUI.Setup(inventoryItem, this);
                    inventoryItems.Add(inventoryItem);
                    itemDictionary.Add(itemData, inventoryItem);
                    equipmentItem.AddModifiers();
                }
        }

        public void EquipItem(ItemData itemData)
        {
            var equipmentData = itemData as EquipmentItemData;

            InventoryItem oldItem = null;
            
            foreach (var slotUI in equipmentSlots)
            {
                if (slotUI.equipmentType == equipmentData!.equipmentType)
                {
                    if (slotUI.item.itemData != null)
                    {
                        oldItem = slotUI.item;
                        var oldEquipment = oldItem.itemData as EquipmentItemData;
                        UnequipItem(oldEquipment, slotUI);
                        oldEquipment!.RemoveModifiers();
                    }
                    AddItem(equipmentData);
                    generalInventory.RemoveItem(itemData);
                }
            }

            if (oldItem != null)
                generalInventory.AddItem(oldItem.itemData);
        }
        

        public void UnequipItem(EquipmentItemData data, EquipmentSlotUI slotUI)
        {
            if (itemDictionary.TryGetValue(data, out var inventoryItem))
            {
                itemDictionary.Remove(data);
                inventoryItems.Remove(inventoryItem);
                data.RemoveModifiers();
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