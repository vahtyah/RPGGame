using System;
using Item_and_Inventory;
using TMPro;
using UnityEngine;

namespace UI
{
    public class ToolTipUI : MonoBehaviour
    {
        public static ToolTipUI Instance { get; private set; }
        
        [SerializeField] private TextMeshProUGUI itemNameText;
        [SerializeField] private TextMeshProUGUI itemTypeText;
        [SerializeField] private TextMeshProUGUI itemDescription;

        private void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(gameObject);
        }

        private void Start()
        {
            HideToolTip();
        }

        public void ShowToolTip(EquipmentItemData equipmentItem)
        {
            // itemNameText.text = item.itemName;
            // itemTypeText.text = item.equipmentType.ToString();
            // gameObject.SetActive(true);
        }

        public void HideToolTip() => gameObject.SetActive(false);
    }
}