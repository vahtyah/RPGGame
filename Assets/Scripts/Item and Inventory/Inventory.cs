using System;
using System.Collections.Generic;
using Item_and_Inventory;
using UI;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance { get; private set; }

    public List<InventoryItem> equipment;
    public Dictionary<ItemDataEquipment, InventoryItem> equipmentDictionary;
    
    public List<InventoryItem> inventory;
    public Dictionary<ItemData, InventoryItem> inventoryDictionary;

    public List<InventoryItem> stash;
    public Dictionary<ItemData, InventoryItem> stashDictionary;

    [Header("Inventory UI")]
    [SerializeField] private Transform inventorySlotParent;
    [SerializeField] private Transform stashSlotParent;
    [SerializeField] private Transform equipmentSlotParent;

    [SerializeField] private GameObject itemSlotUIPrefab;
    private List<ItemSlotUI> inventorySlotUIs;
    private List<ItemSlotUI> stashSlotUIs;
    private EquipmentSlotUI[] equipmentSlotUIs; 

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

        equipment = new List<InventoryItem>();
        equipmentDictionary = new Dictionary<ItemDataEquipment, InventoryItem>();

        inventorySlotUIs = new List<ItemSlotUI>();
        stashSlotUIs = new List<ItemSlotUI>();
        equipmentSlotUIs = equipmentSlotParent.GetComponentsInChildren<EquipmentSlotUI>();
    }

    public void EquipItem(ItemData itemData)
    {
        var newEquipment = itemData as ItemDataEquipment;
        var newItem = new InventoryItem(newEquipment);

        ItemDataEquipment oldEquipment = null;
        
        foreach (var item in equipmentDictionary)
        {
            if (item.Key.equipmentType == newEquipment!.equipmentType)
                oldEquipment = item.Key;
        }

        if(oldEquipment != null)
        {
            UnequipItem(oldEquipment);
            AddItem(oldEquipment);
        }

        equipment.Add(newItem);
        equipmentDictionary.Add(newEquipment!, newItem);
        
        
        //Update slot equipment
        foreach (var equipmentSlotUI in equipmentSlotUIs)
        {
            if(newEquipment.equipmentType == equipmentSlotUI.equipmentType)
                equipmentSlotUI.Setup(newItem);
        }
        
        newEquipment.AddModifiers();
        
        RemoveItem(itemData);
    }

    private void UnequipItem(ItemDataEquipment itemToRemove)
    {
        if (equipmentDictionary.TryGetValue(itemToRemove!, out var value))
        {
            equipment.Remove(value);
            equipmentDictionary.Remove(itemToRemove);
            itemToRemove.RemoveModifiers();
        }
    }

    private void UpdateSlotUI() //TODO: fix performance
    {
        for (var i = 0; i < inventory.Count; i++)
        {
            inventorySlotUIs[i].UpdateSlot(inventory[i]);
        }
        
        for (var i = 0; i < stash.Count; i++)
        {
            stashSlotUIs[i].UpdateSlot(stash[i]);
        }
    }

    public void AddItem(ItemData itemData)
    {
        if (itemData.itemType == ItemType.Equipment) AddToInventory(itemData);
        else if (itemData.itemType == ItemType.Material) AddToStash(itemData);

        // UpdateSlotUI();
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
            var newItemSlotUIScript = newItemSlotUI.GetComponent<ItemSlotUI>();
            
            var newItem = new InventoryItem(itemData);
            
            newItemSlotUIScript.Setup(newItem);
            
            stashSlotUIs.Add(newItemSlotUIScript);
            
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
            
            var newItemSlotUIScript = newItemSlotUI.GetComponent<ItemSlotUI>();
            var newItem = new InventoryItem(itemData);
            newItemSlotUIScript.Setup(newItem);


            inventorySlotUIs.Add(newItemSlotUIScript);
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
        

        // UpdateSlotUI();
    }
}