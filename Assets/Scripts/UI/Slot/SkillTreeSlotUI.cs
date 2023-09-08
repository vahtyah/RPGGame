using System;
using System.Linq;
using Player;
using Save_and_Load;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class SkillTreeSlotUI : MonoBehaviour, ISaveManager
    {
        [Header("Skill info")]
        [SerializeField] private string skillName;
        [SerializeField] private int skillPrice;
        [TextArea]
        [SerializeField] private string skillDescription;
        [SerializeField] private Color skillColor;
        [SerializeField] private SkillCooldownUI cooldownUI;
        

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

        private void Awake()
        {
            img = GetComponent<Image>();
            img.color = unlocker ? Color.white:skillColor;
            GetComponent<Button>().onClick.AddListener( (UnlockSkillSlot));
        }

        private void UnlockSkillSlot()
        {
            if (unlocker) return;
            
            if (shouldBeUnlocker.Any(skill => !skill.unlocker)) return;

            if (shouldBeLocker.Any(skill => skill.unlocker)) return;

            if (!PlayerManager.Instance.HasEnoughMoney(skillPrice)) return;
            cooldownUI?.SetImageSkill(img.sprite);
            cooldownUI?.gameObject.SetActive(true);
            img.color = Color.white;
            OnUnlocked();
        }

        private void OnUnlocked()
        {
            unlocker = true;
            onUnlocked?.Invoke(this, EventArgs.Empty);
        }

        public void LoadData(GameData data)
        {
            if (data.skillTree.Contains(skillName))
            {
                OnUnlocked();
            }
        }

        public void SaveData(ref GameData data)
        {
            if(unlocker && !data.skillTree.Contains(skillName))
            {
                data.skillTree.Add(skillName);
            }
        }
    }
}