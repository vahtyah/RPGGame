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

        public override void AddItem(ItemData itemData)
        {
            base.AddItem(itemData);
        }

        public override void SaveData(ref GameData data)
        {
            Debug.Log("Save Data");
            data.generalInventory.Clear();
            base.SaveData(ref data); 
            foreach (var value in itemDictionary)
            {
                data.generalInventory.Add(value.Value);
                Debug.Log(value.Key.itemName);
            }

            foreach (var item in data.generalInventory)
            {
                Debug.Log("item = " + item.itemData.itemName);
            }
        }
    }
}