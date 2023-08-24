using Save_and_Load;
using UI;
using UnityEngine;

namespace Item_and_Inventory.Test
{
    public class PouchInventory : Inventory
    {
        [Header("Equipment UI")]
        [SerializeField] private PouchSlotUI[] pouchSlots;

        protected override void Start()
        {
            pouchSlots = slotParent.GetComponentsInChildren<PouchSlotUI>();
            base.Start();
        }

        public override bool AddItem(ItemData itemData)
        {
            var pouchItemData = itemData as PouchItemData;
            if (itemDictionary.TryGetValue(pouchItemData, out var value))
            {
                value.AddStack();
                return true;
            }

            foreach (var slotUI in pouchSlots)
            {
                if (slotUI.Item == null)
                {
                    var inventoryItem = new Item(pouchItemData, slotUI.AmountText, slotUI);
                    slotUI.Setup(inventoryItem, this);
                    inventoryItems.Add(inventoryItem);
                    itemDictionary.Add(itemData, inventoryItem);
                    return true;
                }
            }

            return false;
        }

        public override void RemoveItem(ItemData itemData)
        {
            if (!itemDictionary.TryGetValue(itemData, out var inventoryItem)) return;
            if (inventoryItem.stackSize <= 1)
            {
                inventoryItems.Remove(inventoryItem);
                itemDictionary.Remove(itemData);
            }
            else
            {
                inventoryItem.RemoveStack();
            }
        }

        public override void LoadData(GameData data)
        {
            base.LoadData(data);
            foreach (var item in data.pouchInventory)
                loadedItems.Add(item);
        }

        public override void SaveData(ref GameData data)
        {
            base.SaveData(ref data);
            data.pouchInventory.Clear();
            foreach (var pair in itemDictionary)
                data.pouchInventory.Add(pair.Value);
        }
    }
}