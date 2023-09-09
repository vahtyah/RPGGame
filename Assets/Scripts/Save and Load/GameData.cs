using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Item_and_Inventory;

namespace Save_and_Load
{
    [System.Serializable]
    public class GameData
    {
        public int currency;
        public SerializableDictionary<Item, int> generalInventory;
        public SerializableDictionary<Item, int> stashInventory;
        public SerializableDictionary<Item, int> equipmentInventory;
        public SerializableDictionary<Item, int> pouchInventory;
        public List<string> skillTree;
        public SerializableDictionary<string, Sprite> skillCooldownImg;

        public GameData()
        {
            currency = 0;
            generalInventory = new SerializableDictionary<Item, int>();
            stashInventory = new SerializableDictionary<Item, int>();
            equipmentInventory = new SerializableDictionary<Item, int>();
            pouchInventory = new SerializableDictionary<Item, int>();
            skillTree = new List<string>();
            skillCooldownImg = new SerializableDictionary<string, Sprite>();
        }
    }
}