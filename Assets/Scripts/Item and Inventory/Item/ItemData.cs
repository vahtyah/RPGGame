using System;
using UnityEditor;
using UnityEngine;

public enum ItemType
{
    Material,
    Equipment,
    Pouch
}
[Serializable]
[CreateAssetMenu(fileName = "New Item Data", menuName = "Data/Item")]
public class ItemData : ScriptableObject
{
    public ItemType itemType;
    public string itemName;
    public Sprite itemIcon;
    public Sprite itemIconInGame;
    [Space] [Range(0,100)] public float dropChance;
}