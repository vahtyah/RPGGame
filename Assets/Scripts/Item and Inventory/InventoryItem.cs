using System;
using UnityEngine;

[Serializable]
public class InventoryItem
{
    public ItemData itemData;
    public int stackSize;

    private ItemSlotUI slotUI;

    public InventoryItem(ItemData itemData, ItemSlotUI slotUI)
    {
        this.itemData = itemData;
        this.slotUI = slotUI;
        this.stackSize = 1;
    }

    public void AddStack()
    {
        stackSize++;
        slotUI.UpdateSlot(this);
    }

    public void RemoveStack()
    {
        stackSize--; 
        slotUI.UpdateSlot(this);
    }

    public ItemSlotUI SlotUI => slotUI;
}