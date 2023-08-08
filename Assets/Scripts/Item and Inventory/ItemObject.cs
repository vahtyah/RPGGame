using System;
using UnityEngine;

public class ItemObject : MonoBehaviour
{
    [SerializeField] private ItemData itemData;
     private Rigidbody2D rb => GetComponent<Rigidbody2D>();
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
        Inventory.Instance.AddItem(itemData);
        Destroy(gameObject);
    }
}