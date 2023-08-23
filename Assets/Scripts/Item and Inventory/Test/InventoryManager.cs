using System;
using UnityEngine;

namespace Item_and_Inventory.Test
{
    public class InventoryManager : MonoBehaviour
    {
        public static InventoryManager Instance { get; private set; }

        public EquipmentInventory equipmentInventory { get; private set; }
        public GeneralInventory generalInventory { get; private set; }
        public PouchInventory pouchInventory { get; private set; }
        public StashInventory stashInventory { get; private set; }

        private void Awake()
        {
            if (Instance) Destroy(gameObject);
            else Instance = this;
        }

        private void Start()
        {
            equipmentInventory = GetComponent<EquipmentInventory>();
            generalInventory = GetComponent<GeneralInventory>();
            pouchInventory = GetComponent<PouchInventory>();
            stashInventory = GetComponent<StashInventory>();
        }

        public bool AddItem(ItemData itemData)
        {
            return itemData.itemType == ItemType.Pouch ? pouchInventory.AddItem(itemData) : generalInventory.AddItem(itemData);
        }
    }
}