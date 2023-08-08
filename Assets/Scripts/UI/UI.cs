using System;
using UnityEngine;

namespace UI
{
    public class UI : MonoBehaviour
    {
        [SerializeField] private GameObject menuUI;
        public ToolTipUI toolTipUI;
        private void Start() { toolTipUI = GetComponentInChildren<ToolTipUI>(); }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.Tab))
                menuUI.gameObject.SetActive(!menuUI.gameObject.activeSelf);
        }

        public void SwitchTo(GameObject elementMenu)
        {
            for (var i = 0; i < menuUI.transform.childCount - 1; i++)
            {
                menuUI.transform.GetChild(i).gameObject.SetActive(false);
            }
            
            elementMenu.gameObject.SetActive(true);
        }
    }
}