using UnityEngine;

namespace Item_and_Inventory
{
    [CreateAssetMenu(fileName = "New Pouch Item Data", menuName = "Data/Item Pouch")]
    public class PouchItemData : ItemData
    {
        public ItemEffect[] itemEffects;
        public float itemCooldown;
        
        public void ExecuteItemEffect(Transform targetTransform)
        {
            foreach (var item in itemEffects)
                item.ExecuteEffect(targetTransform);
        }
    }
}