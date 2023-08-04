using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance { get; private set; }

    public List<InventoryItem> inventory;
    public Dictionary<ItemData, InventoryItem> inventoryDictionary;

    public List<InventoryItem> stash;
    public Dictionary<ItemData, InventoryItem> stashDictionary;

    [Header("Inventory UI")]
    [SerializeField] private Transform inventorySlotParent;
    [SerializeField] private Transform stashSlotParent;

    [SerializeField] private GameObject itemSlotUIPrefab;
    private List<ItemSlotUI> inventorySlotUIs;
    private List<ItemSlotUI> stashSlotUI;

    private void Awake()
    {
        if (Instance) Destroy(gameObject);
        else Instance = this;
    }

    private void Start()
    {
        inventory = new List<InventoryItem>();
        inventoryDictionary = new Dictionary<ItemData, InventoryItem>();

        stash = new List<InventoryItem>();
        stashDictionary = new Dictionary<ItemData, InventoryItem>();

        inventorySlotUIs = new List<ItemSlotUI>();
        stashSlotUI = new List<ItemSlotUI>();
    }

    private void UpdateSlotUI() //TODO: fix performance
    {
        for (var i = 0; i < inventory.Count; i++)
        {
            inventorySlotUIs[i].UpdateSlot(inventory[i]);
        }
        
        for (var i = 0; i < stash.Count; i++)
        {
            stashSlotUI[i].UpdateSlot(stash[i]);
        }
    }

    public void AddItem(ItemData itemData)
    {
        if (itemData.itemType == ItemType.Equipment) AddToInventory(itemData);
        else if (itemData.itemType == ItemType.Material) AddToStash(itemData);

        UpdateSlotUI();
    }

    private void AddToStash(ItemData itemData)
    {
        if (stashDictionary.TryGetValue(itemData, out InventoryItem value))
        {
            value.AddStack();
        }
        else
        {
            var newItemSlotUI = Instantiate(itemSlotUIPrefab, inventorySlotParent);
            stashSlotUI.Add(newItemSlotUI.GetComponent<ItemSlotUI>());
            var newItem = new InventoryItem(itemData);
            stash.Add(newItem);
            stashDictionary.Add(itemData, newItem);
        }
    }

    private void AddToInventory(ItemData itemData)
    {
        if (inventoryDictionary.TryGetValue(itemData, out InventoryItem value))
        {
            value.AddStack();
        }
        else
        {
            var newItemSlotUI = Instantiate(itemSlotUIPrefab, stashSlotParent);
            inventorySlotUIs.Add(newItemSlotUI.GetComponent<ItemSlotUI>());
            var newItem = new InventoryItem(itemData);
            inventory.Add(newItem);
            inventoryDictionary.Add(itemData, newItem);
        }
    }

    public void RemoveItem(ItemData itemData)
    {
        if (inventoryDictionary.TryGetValue(itemData, out var value))
        {
            if (value.stackSize <= 1)
            {
                inventory.Remove(value);
                inventoryDictionary.Remove(itemData);
            }
            else
            {
                value.RemoveStack();
            }
        }
        
        if (stashDictionary.TryGetValue(itemData, out var stashValue))
        {
            if (stashValue.stackSize <= 1)
            {
                stash.Remove(stashValue);
                stashDictionary.Remove(itemData);
            }
            else
            {
                stashValue.RemoveStack();
            }
        }

        UpdateSlotUI();
    }
}