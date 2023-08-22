using System;
using System.Collections.Generic;
using System.Linq;
using Item_and_Inventory;
using Save_and_Load;
using UI;
using UnityEditor;
using UnityEngine;

public class Inventory : MonoBehaviour, ISaveManager
{
    public static Inventory Instance { get; private set; }

    public List<ItemData> startingItem;

    //Equipment
    public List<InventoryItem> equipment;
    public Dictionary<EquipmentItemData, InventoryItem> equipmentDictionary;

    //Inventory
    public List<InventoryItem> inventory;
    public Dictionary<ItemData, InventoryItem> inventoryDictionary;

    //Stash
    public List<InventoryItem> stash;
    public Dictionary<ItemData, InventoryItem> stashDictionary;

    [Header("Inventory UI")]
    [SerializeField] private Transform inventorySlotParent;
    [SerializeField] private Transform stashSlotParent;
    [SerializeField] private Transform equipmentSlotParent;

    /**/
    [SerializeField] private Transform equipmentSlotUI;
    private PouchCooldownUI[] equipmentCooldownUIs;

    [SerializeField] private GameObject itemSlotUIPrefab;
    private List<ItemSlotUI> inventorySlotUIs;
    private List<ItemSlotUI> stashSlotUIs;
    private EquipmentSlotUI[] equipmentSlotUIs;

    [Header("Items cooldown")]
    private float lastTimeUsedFlask;

    private float lastTimeUsedArmor;

    private float flaskCooldown;
    private float armorCooldown;

    [Header("Data base")]
    public List<InventoryItem> loadedItems;

    public List<InventoryItem> loadedEquipment;

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
        equipmentDictionary = new Dictionary<EquipmentItemData, InventoryItem>();

        inventorySlotUIs = new List<ItemSlotUI>();
        stashSlotUIs = new List<ItemSlotUI>();
        equipmentSlotUIs = equipmentSlotParent.GetComponentsInChildren<EquipmentSlotUI>();

        equipmentCooldownUIs = equipmentSlotUI.GetComponentsInChildren<PouchCooldownUI>();
        LoadItemStart();
    }

    private void LoadItemStart()
    {
        foreach (var itemDataEquipment in loadedEquipment)
        {
            for (var i = 0; i < itemDataEquipment.stackSize; i++)
            {
                AddItem(itemDataEquipment.itemData);
            }
        }

        if (loadedItems.Count > 0)
        {
            foreach (var inventoryItem in loadedItems)
            {
                for (var i = 0; i < inventoryItem.stackSize; i++)
                {
                    AddItem(inventoryItem.itemData);
                }
            }

            return;
        }

        foreach (var itemData in startingItem)
        {
            AddItem(itemData);
        }
    }

    public void EquipItem(ItemData itemData)
    {
        // var newEquipment = itemData as ItemDataEquipment;
        // var newItem = new InventoryItem(newEquipment, null);
        //
        // ItemDataEquipment oldEquipment = null;
        //
        // foreach (var item in equipmentDictionary)
        // {
        //     if (item.Key.equipmentType == newEquipment!.equipmentType)
        //         oldEquipment = item.Key;
        // }
        //
        // if (oldEquipment != null)
        // {
        //     UnequipItem(oldEquipment);
        //     AddItem(oldEquipment);
        // }
        //
        // equipment.Add(newItem);
        // equipmentDictionary.Add(newEquipment!, newItem);
        //
        // //Update slot equipment
        // foreach (var equipmentSlotUI in equipmentSlotUIs)
        // {
        //     // if (newEquipment.equipmentType == equipmentSlotUI.equipmentType)
        //         // equipmentSlotUI.Setup(newItem);
        // }
        //
        // newEquipment.AddModifiers();
        //
        // RemoveItem(itemData);
    }

    public void UnequipItem(EquipmentItemData equipmentItemToRemove)
    {
        if (equipmentDictionary.TryGetValue(equipmentItemToRemove!, out var value))
        {
            equipment.Remove(value);
            equipmentDictionary.Remove(equipmentItemToRemove);
            equipmentItemToRemove.RemoveModifiers();
        }
    }

    public void AddItem(ItemData itemData)
    {
        //TODO: make inventory limit slot or not, then no destroy item drop when not enough space
        if (itemData.itemType == ItemType.Equipment)
        {
            var dataEquipment = itemData as EquipmentItemData;
            if (dataEquipment.equipmentType == EquipmentType.Flask)
            {
                AddToEquipmentSlot(dataEquipment);
            }
            else
                AddToInventory(itemData);
        }
        else if (itemData.itemType == ItemType.Material) AddToStash(itemData);
    }

    private void AddToStash(ItemData itemData)
    {
        // if (stashDictionary.TryGetValue(itemData, out InventoryItem value))
        // {
        //     value.AddStack();
        // }
        // else
        // {
        //     var newItemSlotUI = Instantiate(itemSlotUIPrefab, stashSlotParent);
        //     var newItemSlotUIScript = newItemSlotUI.GetComponent<ItemSlotUI>();
        //
        //     var newItem = new InventoryItem(itemData, newItemSlotUIScript.AmountText);
        //
        //     // newItemSlotUIScript.Setup(newItem);
        //
        //     stashSlotUIs.Add(newItemSlotUIScript);
        //
        //     stash.Add(newItem);
        //     stashDictionary.Add(itemData, newItem);
        // }
    }

    private void AddToInventory(ItemData itemData)
    {
        // if (inventoryDictionary.TryGetValue(itemData, out InventoryItem value))
        // {
        //     value.AddStack();
        // }
        // else
        // {
        //     var newItemSlotUI = Instantiate(itemSlotUIPrefab, inventorySlotParent);
        //
        //     var newItemSlotUIScript = newItemSlotUI.GetComponent<ItemSlotUI>();
        //     var newItem = new InventoryItem(itemData, newItemSlotUIScript.AmountText);
        //     // newItemSlotUIScript.Setup(newItem);
        //
        //     inventorySlotUIs.Add(newItemSlotUIScript);
        //     inventory.Add(newItem);
        //     inventoryDictionary.Add(itemData, newItem);
        // }
    }

    private void AddToEquipmentSlot(EquipmentItemData equipmentItemData)
    {
        if (equipmentDictionary.TryGetValue(equipmentItemData, out InventoryItem value))
        {
            value.AddStack();
        }
        else
        {
            foreach (var equipmentCooldownUI in equipmentCooldownUIs)
            {
                if (equipmentCooldownUI.data == null)
                {
                    // var newItem = new InventoryItem(itemData, equipmentCooldownUI.AmountText, );
                    // equipmentCooldownUI.Setup(newItem);
                    // equipment.Add(newItem);
                    // equipmentDictionary.Add(itemData, newItem);
                    // break;
                }

                Debug.Log("Slot equipment full!");
            }
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
                Destroy(value.AmountText.gameObject);
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
                Destroy(stashValue.AmountText.gameObject);
            }
            else
            {
                stashValue.RemoveStack();
            }
        }

        var equipmentData = itemData as EquipmentItemData;
        if (equipmentDictionary.TryGetValue(equipmentData!, out var equipmentValue))
        {
            if (equipmentValue.stackSize <= 1)
            {
                equipment.Remove(equipmentValue);
                equipmentDictionary.Remove(equipmentData);
            }
            else
            {
                equipmentValue.RemoveStack();
            }
        }
    }

    public bool CanCraft(EquipmentItemData equipmentItemToCraft, List<InventoryItem> requiredMaterials)
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

        AddItem(equipmentItemToCraft);
        Debug.Log("Here is your item " + equipmentItemToCraft.itemName);
        return true;
    }

    public List<InventoryItem> Equipment => equipment;

    public List<InventoryItem> Stash => stash;

    public EquipmentItemData GetEquipmentByType(EquipmentType type)
    {
        EquipmentItemData equipmentItem = null;
        foreach (var item in equipmentDictionary.Where(item => item.Key.equipmentType == type))
        {
            equipmentItem = item.Key;
        }

        return equipmentItem;
    }

    public void UseFlask()
    {
        var currentFlask = GetEquipmentByType(EquipmentType.Flask);
        if (!currentFlask) return;
        var canUseFlask = Time.time > lastTimeUsedFlask + flaskCooldown;

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

    public void LoadData(GameData data)
    {
        var itemDatabases = GetItemDatabase();

        // foreach (var pair in data.inventory)
        // {
        //     if (!itemDatabases.TryGetValue(pair.Key, out var itemData)) continue;
        //     var itemToLoad = new InventoryItem(itemData, null) //TODO: Save slot 
        //     {
        //         stackSize = pair.Value
        //     };
        //
        //     loadedItems.Add(itemToLoad);
        // }
        //
        // foreach (var pair in data.equipment)
        //     if (itemDatabases.TryGetValue(pair.Key, out var itemEquipmentData))
        //     {
        //         var itemToLoad = new InventoryItem(itemEquipmentData, null) //TODO: Save slot 
        //         {
        //             stackSize = pair.Value
        //         };
        //         loadedEquipment.Add(itemToLoad);
        //     }
    }

    public void SaveData(ref GameData data)
    {
        // data.inventory.Clear();
        // data.equipment.Clear();
        //
        // foreach (var value in inventoryDictionary)
        // {
        //     data.inventory.Add(value.Key.itemID, value.Value.stackSize);
        // }
        //
        // foreach (var value in stashDictionary)
        // {
        //     data.inventory.Add(value.Key.itemID, value.Value.stackSize);
        // }
        //
        // foreach (var value in equipmentDictionary)
        // {
        //     data.equipment.Add(value.Key.itemID, value.Value.stackSize);
        // }
    }

    private Dictionary<string, ItemData> GetItemDatabase()
    {
        var itemDatabase = new Dictionary<string, ItemData>();
        var assetNames = AssetDatabase.FindAssets("", new[] { "Assets/Data/Items" });

        foreach (var assetName in assetNames)
        {
            var SOpath = AssetDatabase.GUIDToAssetPath(assetName);
            var itemData = AssetDatabase.LoadAssetAtPath<ItemData>(SOpath);
            if (itemData)
                itemDatabase.Add(itemData.itemID, itemData);
        }

        return itemDatabase;
    }
}