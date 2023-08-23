using System;
using System.Collections.Generic;
using Save_and_Load;
using UI;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

namespace Item_and_Inventory
{
    public class Inventory : MonoBehaviour, ISaveManager
    {
        
        public List<ItemData> startingItem;
        public List<Item> inventoryItems;
        public Dictionary<ItemData, Item> itemDictionary;
        private List<ItemSlotUI> slotUIs;

        [Header("Inventory UI")]
        [SerializeField] protected Transform slotParent;
        [SerializeField] private GameObject itemSlotUIPrefab;
        
        
        [Header("Data base")]
        public List<Item> loadedItems;

        protected virtual void Start()
        {
            inventoryItems = new List<Item>();
            itemDictionary = new Dictionary<ItemData, Item>();
            slotUIs = new List<ItemSlotUI>();
            
            LoadItemStart();
        }

        protected virtual void LoadItemStart()
        {
            if (loadedItems.Count > 0)
            {
                foreach (var item in loadedItems)
                    for (var i = 0; i < item.stackSize; i++)
                        AddItem(item.itemData);
                return;
            }

            foreach (var itemData in startingItem)
            {
                Debug.Log(itemData.itemName);
                AddItem(itemData);
            }
        }

        public virtual bool AddItem(ItemData itemData)
        {
            if (itemDictionary.TryGetValue(itemData, out var value))
            {
                value.AddStack();
            }
            else
            {
                var newItemSlotUI = Instantiate(itemSlotUIPrefab, slotParent);
                
                var newItemSlotUIScript = newItemSlotUI.GetComponent<ItemSlotUI>();
                var newItem = new Item(itemData, newItemSlotUIScript.AmountText, newItemSlotUIScript);
                newItemSlotUIScript.Setup(newItem, this);

                slotUIs.Add(newItemSlotUIScript);
                inventoryItems.Add(newItem);
                itemDictionary.Add(itemData, newItem);
            }

            return true;
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