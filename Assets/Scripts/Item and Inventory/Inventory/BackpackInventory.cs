using Save_and_Load;
using UnityEngine;

namespace Item_and_Inventory.Test
{
    public class BackpackInventory : Inventory
    {
        public override void MoveItemButtonOnClick()
        {
            if (itemSelected == null) return;
            inventory.stashInventory.AddItems(itemSelected);
            base.MoveItemButtonOnClick();
        }

        public override void LoadData(GameData data)
        {
            base.LoadData(data);
            foreach (var item in data.generalInventory)
                loadedItems.Add(item);
        }

        public override void SaveData(ref GameData data)
        {
            data.generalInventory.Clear();
            base.SaveData(ref data); 
            foreach (var value in itemDictionary)
                data.generalInventory.Add(value.Value);
        }
    }
}