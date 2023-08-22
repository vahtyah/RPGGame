using UI;
using UnityEngine;

namespace Item_and_Inventory.Test
{
    public class PouchInventory : Inventory1
    {
        [Header("Equipment UI")]
        [SerializeField] private PouchSlotUI[] pouchSlots;

        protected override void Start()
        {
            pouchSlots = slotParent.GetComponentsInChildren<PouchSlotUI>();
            base.Start();
            Debug.Log("Start" + itemDictionary);
        }

        public override void AddItem(ItemData itemData)
        {
            var pouchItemData = itemData as PouchItemData;
            Debug.Log("Add Item" + itemDictionary);

            if (itemDictionary.TryGetValue(pouchItemData , out var value))
            {
                value.AddStack();
            }
            else
            {
                foreach (var slotUI in pouchSlots)
                {
                    if (slotUI.item.itemData == null)
                    {
                        var inventoryItem = new InventoryItem(pouchItemData, slotUI.AmountText, slotUI);
                        slotUI.Setup(inventoryItem, this);
                        inventoryItems.Add(inventoryItem);
                        itemDictionary.Add(itemData, inventoryItem);
                        return;
                    }
                }
            }
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
    }
}