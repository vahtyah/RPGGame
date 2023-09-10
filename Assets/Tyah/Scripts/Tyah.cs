using System;
using UnityEngine;
using UnityEngine.UI;

namespace Tyah.Scripts
{
    public class Tyah : MonoBehaviour
    {
        private static Tyah Instance { get; set; }
        [Header("Log")]
        public Transform debugParent;

        private void Awake()
        {
            Instance = this; 
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.M))
            {
                Tyah.Log(
                    "tuande dadkfa aslkdfj askdfjkas as dfjk askdfjkas  skdfjaksd  sdfjaskd asjdfkajsd kasdjfkasd");
            }
        }

        public static void Log(string text)
        {
            Scripts.Log.Create(Instance.debugParent, text);
        }
    }
}