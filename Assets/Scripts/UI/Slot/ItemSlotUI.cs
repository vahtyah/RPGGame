using System;
using Item_and_Inventory;
using Item_and_Inventory.Test;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemSlotUI : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] protected Image itemImage;
    [SerializeField] private TextMeshProUGUI amountText;
    [SerializeField] private Transform itemSelectedUI;
    
    
    protected InventoryManager inventoryManager;
    protected Inventory inventory;
    public Item item;
    protected RectTransform rectTransform;

    protected virtual void Start()
    {
        inventoryManager = InventoryManager.Instance;
        rectTransform = GetComponent<RectTransform>();
    }

    public virtual void Setup(Item item, Inventory inventory)
    {
        this.item = item;
        itemImage.sprite = item.itemData.itemIcon;
        this.inventory = inventory;
        UpdateSlot(item);
    }
    
    public void UpdateSlot(Item item)
    {
        amountText.text = item.stackSize > 1 ? item.stackSize.ToString() : "";
    }

    public virtual void OnPointerDown(PointerEventData eventData)
    {
        if(inventory == null) return; //Disable items that are not in the inventory
        if (inventory == inventoryManager.stashInventory)
        {
            inventoryManager.stashInventory.ItemSelected = item;
            inventoryManager.ShowItemSelectedUI(transform.position);
            return;
        }
        // if (Input.GetKey(KeyCode.LeftControl))
        // {   
        //     inventory.RemoveItem(item.itemData);
        //     return;
        // }
        if (item.itemData.itemType == ItemType.Equipment)
        {
            inventoryManager.equipmentInventory.EquipItem(item.itemData);
        }
    }

    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        if (item.itemData == null) return;
        ToolTipUI.Instance.ShowToolTip(item.itemData, rectTransform);
    }

    public virtual void OnPointerExit(PointerEventData eventData)
    {
        if (item.itemData == null) return;
        ToolTipUI.Instance.HideToolTip();
    }

    public TextMeshProUGUI AmountText => amountText;
}