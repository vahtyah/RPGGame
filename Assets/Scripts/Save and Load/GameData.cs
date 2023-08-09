using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Save_and_Load
{
    [System.Serializable]
    public class GameData
    {
        public int currency;
        public SerializableDictionary<string, int> inventory;
        public List<string> skillTree;
        public List<string> equipmentID;

        public GameData()
        {
            currency = 0;
            inventory = new SerializableDictionary<string, int>();
            equipmentID = new List<string>();
            skillTree = new List<string>();
        }
    }
}