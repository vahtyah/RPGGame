using System;
using Unity.VisualScripting;
using UnityEngine;

namespace Skill.Sword
{
    public class Sword : MonoBehaviour
    {
        public Animator anim { get; private set; }
        public Rigidbody2D rb { get; private set; }
        public CircleCollider2D cd { get; private set; }

        public SwordSkillTest swordSkill { get; private set; }
        private SwordSkillTypeMachine typeMachine;
        private RegularSwordSkillType regularSwordSkillType;

        private void Awake()
        {
            cd = GetComponent<CircleCollider2D>();
            rb = GetComponent<Rigidbody2D>();
            anim = GetComponentInChildren<Animator>();
            
            swordSkill = SkillManager.Instance.swordSkillTest;
            typeMachine = new SwordSkillTypeMachine();

            regularSwordSkillType = new RegularSwordSkillType(swordSkill, this);
        }

        public void Setup(SwordType swordType)
        {
            SetupTypeSword(swordType);
        }

        private void SetupTypeSword(SwordType swordType)
        {
            if (swordType == SwordType.Regular)
            {
                typeMachine.CurrentType = regularSwordSkillType;
            }
        }

        private void Update()
        {
            typeMachine.CurrentType?.Update();
        }
    }
}