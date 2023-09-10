using Save_and_Load;
using UnityEngine;

namespace Item_and_Inventory.Test
{
    public class BackpackInventory : Inventory
    {
        [Header("Backpack UI")]
        [SerializeField] private GameObject button;
        [SerializeField] private GameObject equipButton;

        protected override void Start()
        {
            base.Start();
            Tyah.Scripts.Tyah.Log("Start backpack");
        }

        public override void MoveItemButtonOnClick()
        {
            if (itemSelected == null) return;
            inventory.stashInventory.AddItems(itemSelected);
            base.MoveItemButtonOnClick();
        }

        public void EquipItemButtonOnClick()
        {
            inventory.equipmentInventory.EquipItem(itemSelected.itemData);
            SelectItem(null);
        }

        public override void SelectItem(Item itemToSelect)
        {
            base.SelectItem(itemToSelect);
            if(itemSelected == null) button.SetActive(false);
            else
            {
                button.SetActive(true);
                equipButton.SetActive(itemToSelect.itemData.itemType == ItemType.Equipment);
            }
        }

        public override void LoadData(GameData data)
        {
            base.LoadData(data);
            foreach (var item in data.generalInventory)
                loadedItems.Add(item);
            Tyah.Scripts.Tyah.Log("LoadData");
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