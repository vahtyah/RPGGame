using UnityEngine;

namespace Item_and_Inventory
{
    public class ItemObjectTrigger : MonoBehaviour
    {
        private ItemObject itemObject => GetComponentInParent<ItemObject>();
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.GetComponent<Player.Player>() != null)
            {
                Debug.Log("take drop item!");
               itemObject.PickUpItem();
            }
        }
    }
}