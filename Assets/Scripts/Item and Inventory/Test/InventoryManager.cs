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
            // if (Instance) Destroy(gameObject);
            // else Instance = this;

            Instance = this;
        }

        private void Start()
        {
            equipmentInventory = GetComponent<EquipmentInventory>();
            generalInventory = GetComponent<GeneralInventory>();
            pouchInventory = GetComponent<PouchInventory>();
            stashInventory = GetComponent<StashInventory>();
        }

        public void AddItem(ItemData itemData)
        {
            if(itemData.itemType == ItemType.Pouch)
                pouchInventory.AddItem(itemData);
            else
                generalInventory.AddItem(itemData);
        }
    }
}