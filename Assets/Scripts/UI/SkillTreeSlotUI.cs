using System;
using System.Linq;
using Player;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class SkillTreeSlotUI : MonoBehaviour
    {
        [Header("Skill info")]
        [SerializeField] private string skillName;
        [SerializeField] private int skillPrice;
        [TextArea]
        [SerializeField] private string skillDescription;
        [SerializeField] private Color lockSkillColor;

        public bool unlocker;
        [Header("Unlock conditions")]
        [SerializeField] private SkillTreeSlotUI[] shouldBeUnlocker;
        [SerializeField] private SkillTreeSlotUI[] shouldBeLocker;

        public event EventHandler onUnlocked;

        private Image img;

        private void OnValidate()
        {
            gameObject.name = "Skill - " + skillName;
        }

        private void Start()
        {
            img = GetComponent<Image>();
            img.color = lockSkillColor;
            GetComponent<Button>().onClick.AddListener( (() =>
            {
                UnlockSkillSlot();
            }));
        }

        private void UnlockSkillSlot()
        {
            if (shouldBeUnlocker.Any(skill => !skill.unlocker)) return;

            if (shouldBeLocker.Any(skill => skill.unlocker)) return;

            if (!PlayerManager.Instance.HasEnoughMoney(skillPrice)) return;
            
            unlocker = true;
            img.color = Color.white;
            OnUnlocked();
        }

        protected virtual void OnUnlocked() { onUnlocked?.Invoke(this, EventArgs.Empty); }
    }
}