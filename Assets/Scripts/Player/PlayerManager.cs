using System;
using UnityEngine;

namespace Player
{
    public class PlayerManager : MonoBehaviour
    {
        public static PlayerManager Instance { get; private set; }
        public Player player;

        public int currency;

        private void Awake()
        {
            if(Instance) Destroy(gameObject);
            else Instance = this; 
        }
        public bool HasEnoughMoney(int price)
        {
            if (price > currency) return false;
            currency -= price;
            return true;
        }
    }
}