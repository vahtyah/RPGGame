using System;
using TMPro;

namespace Item_and_Inventory
{
    [Serializable]
    public class Item
    {
        public ItemData itemData;
        public int stackSize;

        private TextMeshProUGUI _amountText;
        public ItemSlotUI itemSlotUI;

        public Item(ItemData itemData, ItemSlotUI itemSlotUI = null)
        {
            this.itemData = itemData;
            this.itemSlotUI = itemSlotUI;
            _amountText = itemSlotUI?.AmountText;
            stackSize = 1;
        }

        public void AddStack(int size = 1)
        {
            stackSize += size;
            UpdateAmountText();
        }

        public void RemoveStack(int size = 1)
        {
            stackSize -= size;
            UpdateAmountText();
        }

        private void UpdateAmountText()
        {
            _amountText.text = stackSize <= 1 ? "" : stackSize.ToString();
        }
    }
}