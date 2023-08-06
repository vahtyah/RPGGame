using System;
using System.Collections.Generic;
using System.Linq;
using Item_and_Inventory;
using UI;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance { get; private set; }

    public List<ItemData> startingItem;
    
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

    [Header("Items cooldown")]
    private float lastTimeUsedFlask;
    private float lastTimeUsedArmor;

    private float flaskCooldown;
    private float armorCooldown;

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
        
        foreach (var itemData in startingItem)
        {
            AddItem(itemData);
        }
    }

    public void EquipItem(ItemData itemData)
    {
        var newEquipment = itemData as ItemDataEquipment;
        var newItem = new InventoryItem(newEquipment, null);

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

    public void UnequipItem(ItemDataEquipment itemToRemove)
    {
        if (equipmentDictionary.TryGetValue(itemToRemove!, out var value))
        {
            equipment.Remove(value);
            equipmentDictionary.Remove(itemToRemove);
            itemToRemove.RemoveModifiers();
        }
    }

    public void AddItem(ItemData itemData)
    {
        //TODO: make inventory limit slot or not, then no destroy item drop when not enough space
        
        if (itemData.itemType == ItemType.Equipment) AddToInventory(itemData);
        else if (itemData.itemType == ItemType.Material) AddToStash(itemData);
    }

    private void AddToStash(ItemData itemData)
    {
        if (stashDictionary.TryGetValue(itemData, out InventoryItem value))
        {
            value.AddStack();
        }
        else
        {
            var newItemSlotUI = Instantiate(itemSlotUIPrefab, stashSlotParent);
            var newItemSlotUIScript = newItemSlotUI.GetComponent<ItemSlotUI>();
            
            var newItem = new InventoryItem(itemData, newItemSlotUIScript);
            
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
            var newItemSlotUI = Instantiate(itemSlotUIPrefab, inventorySlotParent);
            
            var newItemSlotUIScript = newItemSlotUI.GetComponent<ItemSlotUI>();
            var newItem = new InventoryItem(itemData, newItemSlotUIScript);
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
                Destroy(value.SlotUI.gameObject);
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
                Destroy(stashValue.SlotUI.gameObject);
            }
            else
            {
                stashValue.RemoveStack();
            }
        }
    }

    public bool CanCraft(ItemDataEquipment itemToCraft, List<InventoryItem> requiredMaterials)
    {
        List<InventoryItem> materialsToRemove = new List<InventoryItem>();
        for (int i = 0; i < requiredMaterials.Count; i++)
        {
            if (stashDictionary.TryGetValue(requiredMaterials[i].itemData, out var value))
            {
                if (value.stackSize < requiredMaterials[i].stackSize)
                {
                    Debug.Log("Not enough materials!");
                    return false;
                }
                else
                {
                    materialsToRemove.Add(value);
                }
            }
            else
            {
                Debug.Log("Not enough materials!");
                return false;
            }
        }

        for (int i = 0; i < materialsToRemove.Count; i++)
        {
            RemoveItem(materialsToRemove[i].itemData);
        }
        
        AddItem(itemToCraft);
        Debug.Log("Here is your item " + itemToCraft.itemName);
        return true;
    }

    public List<InventoryItem> Equipment => equipment;

    public List<InventoryItem> Stash => stash;

    public ItemDataEquipment GetEquipmentByType(EquipmentType type)
    {
        ItemDataEquipment equipmentItem = null;
        foreach (var item in equipmentDictionary.Where(item => item.Key.equipmentType == type))
        {
            equipmentItem = item.Key;
        }

        return equipmentItem;
    }

    public void UseFlask()
    {
        var currentFlask = GetEquipmentByType(EquipmentType.Flask);
        if(!currentFlask) return;
        bool canUseFlask = Time.time > lastTimeUsedFlask + flaskCooldown;

        if (canUseFlask)
        {
            flaskCooldown = currentFlask.itemCooldown;
            currentFlask.ExecuteItemEffect(null);
            lastTimeUsedFlask = Time.time;
        }
        else
        {
            Debug.Log("Flash on cooldown!");
        }
    }

    public bool CanUseArmor()
    {
        var currentArmor = GetEquipmentByType(EquipmentType.Armor);
        if (Time.time > lastTimeUsedArmor + armorCooldown)
        {
            armorCooldown = currentArmor.itemCooldown;
            lastTimeUsedArmor = Time.time;
            return true;
        }

        Debug.Log("Armor on cooldown!");
        return false;
    }
}