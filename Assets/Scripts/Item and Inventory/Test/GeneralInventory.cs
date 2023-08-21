using Save_and_Load;
using UnityEngine;

namespace Item_and_Inventory.Test
{
    public class GeneralInventory : Inventory1
    {
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