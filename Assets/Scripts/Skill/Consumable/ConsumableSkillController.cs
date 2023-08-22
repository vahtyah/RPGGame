using System;
using UI;
using UnityEngine;

namespace Skill.Consumable
{
    public class ConsumableSkillController : MonoBehaviour
    {
        [SerializeField] private KeyCode keyCode;
        public event EventHandler<PouchSlotUI> keyDown;
        private PouchSlotUI pouchSlotUI;

        private void Start() { pouchSlotUI = GetComponent<PouchSlotUI>(); }

        private void Update()
        {
            if (Input.GetKeyDown(keyCode))
            {
                OnKeyDown(pouchSlotUI);
            }
        }

        protected virtual void OnKeyDown(PouchSlotUI e) { keyDown?.Invoke(this, e); }
    }
}