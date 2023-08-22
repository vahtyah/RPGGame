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
    protected InventoryManager inventoryManager;
    protected Inventory1 inventory;
    public InventoryItem item;

    private void Start() { inventoryManager = InventoryManager.Instance; }

    public virtual void Setup(InventoryItem item, Inventory1 inventory)
    {
        this.item = item;
        itemImage.sprite = item.itemData.itemIcon;
        this.inventory = inventory;
        UpdateSlot(item);
    }
    
    public void UpdateSlot(InventoryItem item)
    {
        amountText.text = item.stackSize > 1 ? item.stackSize.ToString() : "";
    }

    public virtual void OnPointerDown(PointerEventData eventData)
    {
        if (Input.GetKey(KeyCode.LeftControl))
        {   
            inventory.RemoveItem(item.itemData);
            return;
        }
        if (item.itemData.itemType == ItemType.Equipment)
        {
            inventoryManager.equipmentInventory.EquipItem(item.itemData);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (item == null) return;
        ToolTipUI.Instance.ShowToolTip(item.itemData as EquipmentItemData);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (item == null) return;
        ToolTipUI.Instance.HideToolTip();
    }

    public TextMeshProUGUI AmountText => amountText;
}