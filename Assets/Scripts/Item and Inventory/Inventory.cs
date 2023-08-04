using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance { get; private set; }
    public List<InventoryItem> inventoryItems;
    public Dictionary<ItemData, InventoryItem> inventoryDictionary;
    
    [Header("Inventory UI")]
    [SerializeField] private Transform inventorySlotParent;
    [SerializeField] private GameObject itemSlotUIPrefab;
    private List<ItemSlotUI> itemSlotUIs;

    private void Awake()
    {
        if (Instance) Destroy(gameObject);
        else Instance = this;
    }

    private void Start()
    {
        inventoryItems = new List<InventoryItem>();
        inventoryDictionary = new Dictionary<ItemData, InventoryItem>();

        itemSlotUIs = new List<ItemSlotUI>();
    }

    private void UpdateSlotUI() //TODO: fix performance
    {
        for (var i = 0; i < inventoryItems.Count; i++)
        {
            itemSlotUIs[i].UpdateSlot(inventoryItems[i]);
        }
    }

    public void AddItem(ItemData itemData)
    {
        if (inventoryDictionary.TryGetValue(itemData, out InventoryItem value))
        {
            value.AddStack();
        }
        else
        {
            var newItemSlotUI = Instantiate(itemSlotUIPrefab, inventorySlotParent);
            itemSlotUIs.Add(newItemSlotUI.GetComponent<ItemSlotUI>());
            var newItem = new InventoryItem(itemData);
            inventoryItems.Add(newItem);
            inventoryDictionary.Add(itemData, newItem);
        }
        
        UpdateSlotUI();
    }

    public void RemoveItem(ItemData itemData)
    {
        if (inventoryDictionary.TryGetValue(itemData, out var value))
        {
            if (value.stackSize <= 1)
            {
                inventoryItems.Remove(value);
                inventoryDictionary.Remove(itemData);
            }
            else
            {
                value.RemoveStack();
            }
            UpdateSlotUI();
        }
    }
}