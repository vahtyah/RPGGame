using System;
using Save_and_Load;
using UnityEngine;

namespace Player
{
    public class PlayerManager : MonoBehaviour, ISaveManager
    {
        public static PlayerManager Instance { get; private set; }
        public Player player;

        public int currency;
        public event EventHandler<int> onCurrencyChanged;

        private void Awake()
        {
            if(Instance) Destroy(gameObject);
            else Instance = this; 
        }

        private void Start()
        {
            OnCurrencyChanged(currency);
        }

        public bool HasEnoughMoney(int price)
        {
            if (price > currency) return false;
            currency -= price;
            OnCurrencyChanged(currency);
            return true;
        }

        protected virtual void OnCurrencyChanged(int e) { onCurrencyChanged?.Invoke(this, e); }

        public void LoadData(GameData data)
        {
            currency = data.currency;
        }

        public void SaveData(ref GameData data)
        {
            data.currency = currency;
        }
    }
}