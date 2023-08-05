﻿using UnityEngine;

namespace Item_and_Inventory
{
    [CreateAssetMenu(fileName = "New Item Data", menuName = "Data/Item Effect")]
    public class ItemEffect : ScriptableObject
    {
        public virtual void ExecuteEffect()
        {
            Debug.Log("Effect execute!");
        }
    }
}