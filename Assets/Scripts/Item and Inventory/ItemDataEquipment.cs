using UnityEngine;

namespace Item_and_Inventory
{
    public enum EquipmentType
    {
        Weapon,
        Armor,
        Amulet,
        Flask
    }
    
    [CreateAssetMenu(fileName = "New Item Data Equipment", menuName = "Data/Item Equipment")]
    public class ItemDataEquipment : ItemData
    {
        public EquipmentType equipmentType;
    }
}