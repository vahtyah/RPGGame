using System;
using UnityEngine;

namespace Item_and_Inventory.Test
{
    public class InventoryManager : MonoBehaviour
    {
        public Transform itemSelectedUI;
        public static InventoryManager Instance { get; private set; }

        public Inventory inventory { get; set; }
        public EquipmentInventory equipmentInventory { get; private set; }
        public BackpackInventory backpackInventory { get; private set; }
        public PouchInventory pouchInventory { get; private set; }
        public StashInventory stashInventory { get; private set; }
        public CraftInventory craftInventory { get; private set; }

        private void Awake()
        {
            if (Instance) Destroy(gameObject);
            else Instance = this;
        }

        private void Start()
        {
            equipmentInventory = GetComponent<EquipmentInventory>();
            backpackInventory = GetComponent<BackpackInventory>();
            pouchInventory = GetComponent<PouchInventory>();
            stashInventory = GetComponent<StashInventory>();
            craftInventory = GetComponent<CraftInventory>();
            inventory = GetComponent<Inventory>();
        }

        public bool AddItem(ItemData itemData)
        {
            return itemData.itemType == ItemType.Pouch ? pouchInventory.AddItem(itemData) : backpackInventory.AddItem(itemData);
        }

        public void ShowItemSelectedUI(Vector3 position)
        {
            itemSelectedUI.gameObject.SetActive(true);
            itemSelectedUI.position = position;
        }


        public void HideItemSelectedUI()
        {
            itemSelectedUI.gameObject.SetActive(false);
        }
    }
}