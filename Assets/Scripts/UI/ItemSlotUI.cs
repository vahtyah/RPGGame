using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemSlotUI : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] protected Image itemImage;
    [SerializeField] private TextMeshProUGUI itemText;

    public InventoryItem item;  

    public virtual void Setup(InventoryItem item)
    {
        this.item = item;
        itemImage.sprite = item.itemData.icon;
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
}