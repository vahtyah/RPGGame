using System;
using Item_and_Inventory.Test;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    public class UI : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] private GameObject menuUI;
        public ToolTipUI toolTipUI;

        private InventoryManager inventory;

        private void Start()
        {
            toolTipUI = GetComponentInChildren<ToolTipUI>(); 
            inventory = InventoryManager.Instance;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                menuUI.gameObject.SetActive(!menuUI.gameObject.activeSelf);
                Time.timeScale = menuUI.gameObject.activeSelf ? 0 : 1;
                inventory.inventory.SelectItem(null);
            }
        }

        public void SwitchTo(GameObject elementMenu)
        {
            for (var i = 0; i < menuUI.transform.childCount - 1; i++)
            {
                menuUI.transform.GetChild(i).gameObject.SetActive(false);
            }

            inventory.inventory.SelectItem(null);
            elementMenu.gameObject.SetActive(true);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            inventory.inventory.SelectItem(null);
        }
    }
}