using System;
using Item_and_Inventory;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemSlotUI : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] protected Image itemImage;
    [SerializeField] private TextMeshProUGUI itemText;

    public InventoryItem item;  

    public virtual void Setup(InventoryItem item)
    {
        this.item = item;
        itemImage.sprite = item.itemData.itemIcon;
        UpdateSlot(item);
    }
    
    public void UpdateSlot(InventoryItem item)
    {
        itemText.text = item.stackSize > 1 ? item.stackSize.ToString() : "";
    }

    public virtual void OnPointerDown(PointerEventData eventData)
    {
        if (Input.GetKey(KeyCode.LeftControl))
        {
            Inventory.Instance.RemoveItem(item.itemData);
            return;
        }
        if (item.itemData.itemType == ItemType.Equipment)
        {
            Inventory.Instance.EquipItem(item.itemData);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!item.itemData) return;
        ToolTipUI.Instance.ShowToolTip(item.itemData as ItemDataEquipment);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!item.itemData) return;
        ToolTipUI.Instance.HideToolTip();
    }

    public TextMeshProUGUI AmountText => itemText;
}