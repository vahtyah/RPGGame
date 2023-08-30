using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Item_and_Inventory.Test
{
    public class CraftInventory : Inventory
    {
        [Header("Craft UI")]
        [SerializeField] private GameObject CraftInfoItem;

        [SerializeField] private GameObject itemMaterialPrefab;
        [SerializeField] private Transform requiredMaterialsParent;
        [SerializeField] private ItemSlotUI itemToCraft;
        [SerializeField] private Button craftButton;

        private EquipmentItemData itemData;

        protected override void Start()
        {
            loadedItems = GetItemDatabase();
            craftButton.onClick.AddListener(() =>
            {
                if (!CanCraft(itemData, itemData.craftingMaterials))
                    Debug.Log("cuts");
            });
            base.Start();
        }

        public void ShowCraftInfo(EquipmentItemData itemData, List<Item> requiredMaterials)
        {
            CraftInfoItem.SetActive(true);
            this.itemData = itemData;
            var item = new Item(itemData, null, null);
            itemToCraft.Setup(item, null);
            var requiredMaterialsTmp = new List<Item>(requiredMaterials);
            var matslots = requiredMaterialsParent.GetComponentsInChildren<ItemSlotUI>();

            foreach (var slotUI in matslots)
            {
                if (requiredMaterialsTmp.Count <= 0)
                {
                    Destroy(slotUI.gameObject);
                    continue;
                }

                slotUI.Setup(requiredMaterialsTmp.First(), null);
                requiredMaterialsTmp.RemoveAt(0);
            }

            foreach (var material in requiredMaterialsTmp)
            {
                var mat = Instantiate(itemMaterialPrefab, requiredMaterialsParent);
                var matSlotScript = mat.GetComponent<ItemSlotUI>();
                matSlotScript.Setup(material, null);
            }
        }

        private void HideCraftInfo() { CraftInfoItem.SetActive(false); }

        public bool CanCraft(EquipmentItemData itemData, List<Item> requiredMaterials)
        {
            foreach (var material in requiredMaterials)
            {
                if (inventory.backpackInventory.itemDictionary.TryGetValue(material.itemData, out var item))
                {
                    if (material.stackSize <= item.stackSize) continue;
                    Debug.Log("not enough material!");
                    return false;
                }

                return false;
            }

            foreach (var material in requiredMaterials)
                inventory.backpackInventory.RemoveItem(material.itemData);
            inventory.backpackInventory.AddItem(itemData);
            Debug.Log($"Crafted {itemData.itemName}");
            return true;
        }

        public override void SelectItem(Item itemToSelect)
        {
            //TODO: just inventory run, not craft
            base.SelectItem(itemToSelect);
            if (itemToSelect == null)
            {
                HideCraftInfo();
                return;
            }

            itemData = itemToSelect.itemData as EquipmentItemData;
            ShowCraftInfo(itemData, itemData!.craftingMaterials);
        }

        private List<Item> GetItemDatabase()
        {
            var itemDatabase = new List<Item>();
            var itemDataArray = Resources.LoadAll<ItemData>("Items");

            foreach (var itemData in itemDataArray)
            {
                var item = new Item(itemData, null, null);
                itemDatabase.Add(item);
            }

            return itemDatabase;
        }
    }
}