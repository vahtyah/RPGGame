using System.Collections.Generic;
using UnityEngine;

namespace Item_and_Inventory
{
    public class ItemDrop : MonoBehaviour
    {
        [SerializeField] private int amountOfDrop;
        [SerializeField] private ItemData[] possibleDrop;
         [SerializeField] private List<ItemData> dropList = new();
        
        [SerializeField] private GameObject dropPrefab;

        public void GenerateDrop()
        {
            foreach (var item in possibleDrop)
            {
                if (Random.Range(0, 100) <= item.dropChance)
                {
                    dropList.Add(item);
                }
            }

            for (int i = 0; i < amountOfDrop; i++)
            {
                if (dropList.Count < 1) break;
                foreach (var itemData in dropList)
                {
                    Debug.Log(itemData.itemName);
                }
                var randomIndex = Random.Range(0, dropList.Count);
                Debug.Log("randomIndex = " + randomIndex);
                var randomItem = dropList[randomIndex];
                dropList.Remove(randomItem);
                DropItem(randomItem);
            }
        }
        
        public void DropItem(ItemData itemDrop)
        {
            var newDrop = Instantiate(dropPrefab, transform.position, Quaternion.identity);
            var randomVelocity = new Vector2(Random.Range(-5, 5), Random.Range(12, 15));
            newDrop.GetComponent<ItemObject>().Setup(itemDrop,randomVelocity);
        }
    }
}