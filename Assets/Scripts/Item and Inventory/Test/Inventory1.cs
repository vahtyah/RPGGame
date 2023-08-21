using System;
using System.Collections.Generic;
using Save_and_Load;
using UI;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

namespace Item_and_Inventory
{
    public class Inventory1 : MonoBehaviour, ISaveManager
    {
        
        public List<ItemData> startingItem;
        public List<InventoryItem> inventoryItems;
        public Dictionary<ItemData, InventoryItem> itemDictionary;
        private List<ItemSlotUI> slotUIs;

        [Header("Inventory UI")]
        [SerializeField] protected Transform slotParent;
        [SerializeField] private GameObject itemSlotUIPrefab;
        
        
        [Header("Data base")]
        public List<InventoryItem> loadedItems;

        protected virtual void Start()
        {
            inventoryItems = new List<InventoryItem>();
            itemDictionary = new Dictionary<ItemData, InventoryItem>();
            slotUIs = new List<ItemSlotUI>();
            
            LoadItemStart();
        }

        protected virtual void LoadItemStart()
        {
            if (loadedItems.Count > 0)
            {
                foreach (var inventoryItem in loadedItems)
                {
                    AddItem(inventoryItem.itemData);
                }

                return;
            }

            foreach (var itemData in startingItem)
            {
                AddItem(itemData);
            }
        }

        public virtual void AddItem(ItemData itemData)
        {
            if (itemDictionary.TryGetValue(itemData, out var value))
            {
                value.AddStack();
            }
            else
            {
                var newItemSlotUI = Instantiate(itemSlotUIPrefab, slotParent);
                
                var newItemSlotUIScript = newItemSlotUI.GetComponent<ItemSlotUI>();
                var newItem = new InventoryItem(itemData, newItemSlotUIScript.AmountText, newItemSlotUIScript);
                newItemSlotUIScript.Setup(newItem, this);

                slotUIs.Add(newItemSlotUIScript);
                inventoryItems.Add(newItem);
                itemDictionary.Add(itemData, newItem);
            }
        }

        public virtual void RemoveItem(ItemData itemData)
        {
            if (!itemDictionary.TryGetValue(itemData, out var inventoryItem)) return;
            if (inventoryItem.stackSize <= 1)
            {
                inventoryItems.Remove(inventoryItem);
                itemDictionary.Remove(itemData);
                Destroy(inventoryItem.itemSlotUI.gameObject);//TODO: fix
            }
            else
            {
                inventoryItem.RemoveStack();
            }
        }

        public virtual void LoadData(GameData data)
        {
            
        }

        public virtual void SaveData(ref GameData data)
        {

        }
    }
}