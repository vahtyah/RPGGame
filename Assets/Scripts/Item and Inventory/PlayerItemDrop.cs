using UnityEngine;

namespace Item_and_Inventory
{
    public class PlayerItemDrop : ItemDrop
    {
        [Header("Player's drop")]
        [SerializeField] private float chanceToLoseEquipmentItems;
        [SerializeField] private float chanceToLoseMaterialItems;

        public override void GenerateDrop()
        {
            //TODO: fix
            // var inventory = Inventory.Instance;
            // var currentEquipment = inventory.Equipment;
            // for (var i = 0; i < currentEquipment.Count;)
            // {
            //     var item = currentEquipment[i];
            //     if (Random.Range(0, 100) <= chanceToLoseEquipmentItems)
            //     {
            //         DropItem(item.itemData);
            //         inventory.UnequipItem(item.itemData as EquipmentItemData);
            //         //TODO: change image alpha
            //         currentEquipment = inventory.Equipment;
            //         continue;
            //     }
            //     i++;
            // }
            //
            // var currentMaterials = inventory.Stash;
            // for (var i = 0; i < currentMaterials.Count;)
            // {
            //     var item = currentMaterials[i];
            //     if (Random.Range(0, 100) <= chanceToLoseMaterialItems)
            //     {
            //         DropItem(item.itemData);
            //         inventory.RemoveItem(item.itemData);
            //         currentMaterials = inventory.Equipment;
            //         continue;
            //     }
            //     i++;
            // }
        }
    }
}