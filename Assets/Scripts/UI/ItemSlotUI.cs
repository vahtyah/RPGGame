using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemSlotUI : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] protected Image itemImage;
    [SerializeField] private TextMeshProUGUI itemText;

    private InventoryItem item;

    public virtual void Setup(InventoryItem item)
    {
        this.item = item;
        itemImage.sprite = item.itemData.icon;
        itemText.text = item.stackSize > 1 ? item.stackSize.ToString() : "";
    }
    
    public void UpdateSlot(InventoryItem item)
    {
        if (item != null)
        {
            this.item = item;
            itemImage.sprite = item.itemData.icon;
            itemText.text = item.stackSize > 1 ? item.stackSize.ToString() : "";
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (item.itemData.itemType == ItemType.Equipment)
        {
            Inventory.Instance.EquipItem(item.itemData);
            Destroy(gameObject);
        }
    }
}