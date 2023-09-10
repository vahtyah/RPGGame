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
        public List<Item> generalInventory;
        public List<Item> stashInventory;
        public List<Item> equipmentInventory;
        public List<Item> pouchInventory;
        public List<string> skillTree;
        public SerializableDictionary<string, Sprite> skillCooldownImg;

        public GameData()
        {
            currency = 0;
            generalInventory = new List<Item>();
            stashInventory = new List<Item>();
            equipmentInventory = new List<Item>();
            pouchInventory = new List<Item>();
            skillTree = new List<string>();
            skillCooldownImg = new SerializableDictionary<string, Sprite>();
        }
    }
}