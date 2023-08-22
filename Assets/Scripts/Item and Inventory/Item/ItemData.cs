using System;
using UnityEditor;
using UnityEngine;

public enum ItemType
{
    Material,
    Equipment,
    Pouch
}

[CreateAssetMenu(fileName = "New Item Data", menuName = "Data/Item")]
public class ItemData : ScriptableObject
{
    public ItemType itemType;
    public string itemName;
    public Sprite itemIcon;
    public string itemID;
    [Space] [Range(0,100)] public float dropChance;

    private void OnValidate()
    {
#if UNITY_EDITOR
        var path = AssetDatabase.GetAssetPath(this);
        itemID = AssetDatabase.AssetPathToGUID(path);
#endif
    }
}