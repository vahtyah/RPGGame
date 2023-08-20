using System;
using TMPro;
using UnityEngine;

[Serializable]
public class InventoryItem
{
    public ItemData itemData;
    public int stackSize;

    private TextMeshProUGUI _amountText;

    public InventoryItem(ItemData itemData, TextMeshProUGUI amountText)
    {
        this.itemData = itemData;
        this._amountText = amountText;
        this.stackSize = 1;
    }

    public void AddStack()
    {
        stackSize++;
        _amountText.text = stackSize.ToString();
    }

    public void RemoveStack()
    {
        stackSize--;
        _amountText.text = stackSize <= 1 ? "" : stackSize.ToString();
    }

    public TextMeshProUGUI AmountText => AmountText;
}