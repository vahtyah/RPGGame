using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Save_and_Load
{
    [System.Serializable]
    public class GameData
    {
        public int currency;
        public List<InventoryItem> generalInventory;
        public List<InventoryItem> stashInventory;
        public List<InventoryItem> equipmentInventory;
        public List<InventoryItem> pouchInventory;
        public List<string> skillTree;

        public GameData()
        {
            currency = 0;
            generalInventory = new List<InventoryItem>();
            stashInventory = new List<InventoryItem>();
            equipmentInventory = new List<InventoryItem>();
            pouchInventory = new List<InventoryItem>();
            skillTree = new List<string>();
        }
    }
}