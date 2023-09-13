using System;
using System.Collections.Generic;
using Item_and_Inventory.Test;
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
        
        protected Item itemSelected;
        protected InventoryManager inventory;

        [Header("Inventory UI")]
        [SerializeField] protected Transform slotParent;
        [SerializeField] protected GameObject itemSlotUIPrefab;
        
        
        [Header("Data base")]
        public List<Item> loadedItems;

        protected virtual void Start()
        {
            inventoryItems = new List<Item>();
            itemDictionary = new Dictionary<ItemData, Item>();
            slotUIs = new List<ItemSlotUI>();
            inventory = InventoryManager.Instance;
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
                var newItem = new Item(itemData, newItemSlotUIScript);
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

        public virtual void RemoveItems(Item item)
        {
            if (itemDictionary.ContainsValue(item))
            {
                inventoryItems.Remove(item);
                itemDictionary.Remove(item.itemData);
                Destroy(item.itemSlotUI.gameObject);
            }
        }

        public virtual void AddItems(Item item)
        {
            if (itemDictionary.ContainsKey(item.itemData))
            {
                itemDictionary[item.itemData].AddStack(item.stackSize);
            }
            else
            {
                var newItemSlotUI = Instantiate(itemSlotUIPrefab, slotParent);
                
                var newItemSlotUIScript = newItemSlotUI.GetComponent<ItemSlotUI>();
                var newItem = new Item(item.itemData, newItemSlotUIScript);
                newItemSlotUIScript.Setup(newItem, this);
                Debug.Log(item.stackSize);
                newItem.AddStack(item.stackSize - 1);

                slotUIs.Add(newItemSlotUIScript);
                inventoryItems.Add(newItem);
                itemDictionary.Add(newItem.itemData, newItem);
            }
        }
        
        public virtual void MoveItemButtonOnClick()
        {
            DiscardItemButtonOnClick();
        }

        public void DiscardItemButtonOnClick()
        {
            if(itemSelected == null) return;
            RemoveItems(itemSelected);
            SelectItem(null);
        }

        public virtual void SelectItem(Item itemToSelect)
        {
            itemSelected = itemToSelect;
            if(itemSelected == null) inventory.HideItemSelectedUI();
            else inventory.ShowItemSelectedUI(itemToSelect);
        }

        public virtual void LoadData(GameData data)
        {
            
        }

        public virtual void SaveData(ref GameData data)
        {

        }
    }
}