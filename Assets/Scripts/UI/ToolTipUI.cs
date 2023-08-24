using System;
using Item_and_Inventory;
using TMPro;
using UnityEngine;

namespace UI
{
    public class ToolTipUI : MonoBehaviour
    {
        public static ToolTipUI Instance { get; private set; }

        [SerializeField] private RectTransform canvasRectTransform;
        [SerializeField] private TextMeshProUGUI itemNameText;
        [SerializeField] private TextMeshProUGUI itemTypeText;
        [SerializeField] private TextMeshProUGUI itemDescription;

        private RectTransform rectTransform;
        private RectTransform transformTarget;

        private void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(gameObject);
        }

        private void Start()
        {
            HideToolTip();
            rectTransform = GetComponent<RectTransform>();
        }

        private void Update() { HandleFollowMouse(); }

        private void HandleFollowMouse()
        {
            var mousePosition = Input.mousePosition;
            if (transformTarget == null)
            {
                HideToolTip();
                return;
            }

            if (mousePosition.x < Screen.width / 2)
                rectTransform.pivot = new Vector2(0, .5f);
            else
                rectTransform.pivot = new Vector2(1, .5f);

            if (rectTransform.pivot.x == 0)
            {
                var positionTarget = transformTarget.transform.position.x + transformTarget.rect.width / 2;
                mousePosition.x += (positionTarget - mousePosition.x);
            }
            else
            {
                var positionTarget = transformTarget.transform.position.x - transformTarget.rect.width / 2;
                mousePosition.x -= (mousePosition.x - positionTarget);
            }

            transform.position = mousePosition;
        }

        public void ShowToolTip(ItemData itemData, RectTransform transformTarget)
        {
            itemNameText.text = itemData.itemName;
            if (itemData.itemType == ItemType.Equipment)
                itemTypeText.text = (itemData as EquipmentItemData)!.equipmentType.ToString();
            gameObject.SetActive(true);
            this.transformTarget = transformTarget;
        }

        public void HideToolTip() => gameObject.SetActive(false);
    }
}