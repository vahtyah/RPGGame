using System;
using UnityEngine;

namespace UI
{
    public class EquipmentUI : MonoBehaviour
    {
        private EquipmentSlotUI[] equipmentSlotUis;

        private void Start()
        {
            equipmentSlotUis = transform.GetComponentsInChildren<EquipmentSlotUI>();
        }
    }
}