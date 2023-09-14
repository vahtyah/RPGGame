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
        
        [Header("Skill UI")]
        [SerializeField] private ArrowLineUI[] arrowLine;
        [SerializeField] private Image image;
        

        public bool unlocker;
        [Header("Unlock conditions")]
        [SerializeField] private SkillTreeSlotUI[] shouldBeUnlocker;
        [SerializeField] private SkillTreeSlotUI[] shouldBeLocker;

        public event EventHandler onUnlocked;


        private void OnValidate()
        {
            gameObject.name = "Skill - " + skillName;
        }

        private void Awake()
        {
            image.color = unlocker ? Color.white : skillColor;
            GetComponent<Button>().onClick.AddListener((UnlockSkillSlot));
        }

        private void UnlockSkillSlot()
        {
            if (unlocker) return;
            
            if (shouldBeUnlocker.Any(skill => !skill.unlocker)) return;

            if (shouldBeLocker.Any(skill => skill.unlocker)) return;

            if (!PlayerManager.Instance.HasEnoughMoney(skillPrice))
            {
                NotificationUI.Instance.AddNotification("Not enought soul!",NotificationType.Skill);
                return;
            }

            cooldownUI?.SetImageSkill(image.sprite);
            cooldownUI?.gameObject.SetActive(true);
            
            foreach (var arrowLineUI in arrowLine)
            {
                arrowLineUI.StartFilling();
            }
            
            OnUnlocked();
        }

        private void OnUnlocked()
        {
            unlocker = true;
            image.color = Color.white;
            onUnlocked?.Invoke(this, EventArgs.Empty);
        }

        public void LoadData(GameData data)
        {
            if (data.skillTree.Contains(skillName))
            {
                OnUnlocked();
                foreach (var arrowLineUI in arrowLine)
                {
                    arrowLineUI.SetFill();
                }
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