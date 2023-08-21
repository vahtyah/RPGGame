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
        public SerializableDictionary<string, int> equipment;

        public GameData()
        {
            currency = 0;
            inventory = new SerializableDictionary<string, int>();
            equipment = new SerializableDictionary<string, int>();
            skillTree = new List<string>();
        }
    }
}