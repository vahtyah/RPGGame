using System;
using UnityEngine;

namespace Player
{
    public class PlayerManager : MonoBehaviour
    {
        public static PlayerManager Instance { get; private set; }
        public Player player;

        private void Awake()
        {
            if(Instance) Destroy(gameObject);
            else Instance = this; 
        }
    }
}