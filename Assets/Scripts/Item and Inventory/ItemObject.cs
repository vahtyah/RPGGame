using System;
using Item_and_Inventory.Test;
using UI;
using UnityEngine;

public class ItemObject : MonoBehaviour
{
    [SerializeField] private ItemData itemData;
     private Rigidbody2D rb => GetComponent<Rigidbody2D>();

     private void OnValidate()
     {
         if (!itemData) return;
         GetComponent<SpriteRenderer>().sprite = itemData.itemIconInGame;
         gameObject.name = "Item object - " + itemData.itemName;
     }

     private void UpdateVisualItem()
    {
        if (!itemData) return;
        GetComponent<SpriteRenderer>().sprite = itemData.itemIcon;
        gameObject.name = "Item object - " + itemData.itemName;
    }

    public void Setup(ItemData itemData, Vector2 velocity)
    {
        this.itemData = itemData;
        rb.velocity = velocity;
        UpdateVisualItem();
    }

    public void PickUpItem()
    {
        if (InventoryManager.Instance.AddItem(itemData))
        {
            Destroy(gameObject);
            NotificationUI.Instance.AddNotification($"Picked up {itemData.itemName}",NotificationType.PickUp,$", {itemData.itemName}");
        }
    }
}