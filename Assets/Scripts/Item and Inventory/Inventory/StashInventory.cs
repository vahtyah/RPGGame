using Save_and_Load;
using UnityEngine;

namespace Item_and_Inventory.Test
{
    public class StashInventory : Inventory
    {
        [Header("Stash UI")]
        [SerializeField] private GameObject button;
        public override void MoveItemButtonOnClick()
        {
            if (itemSelected == null) return;
            inventory.backpackInventory.AddItems(itemSelected);
            base.MoveItemButtonOnClick();
        }

        public override void SelectItem(Item itemToSelect)
        {
            base.SelectItem(itemToSelect);
            button.SetActive(itemSelected != null);
        }

        public override void LoadData(GameData data)
        {
            base.LoadData(data);
            foreach (var item in data.stashInventory)
                loadedItems.Add(item);
        }

        public override void SaveData(ref GameData data)
        {
            base.SaveData(ref data);
            data.stashInventory.Clear();
            foreach (var value in itemDictionary)
                data.stashInventory.Add(value.Value);
        }
    }
}