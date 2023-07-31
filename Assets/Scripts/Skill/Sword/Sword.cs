using System;
using Unity.VisualScripting;
using UnityEngine;

namespace Skill.Sword
{
    public class Sword : MonoBehaviour
    {
        
        #region Component

        public Animator anim { get; private set; }
        public Rigidbody2D rb { get; private set; }
        public CircleCollider2D cd { get; private set; }

        #endregion

        #region SkillType

        public SwordSkill swordSkill { get; private set; }
        public SwordSkillTypeMachine typeMachine{ get; private set; }
        private RegularSwordSkillType regularSword;
        private BounceSwordSkillType bounceSword;
        private PierceSwordSkillType pierceSword;
        private SpinSwordSkillType spinSword;

        #endregion

        private void Awake()
        {
            cd = GetComponent<CircleCollider2D>();
            rb = GetComponent<Rigidbody2D>();
            anim = GetComponentInChildren<Animator>();

            swordSkill = SkillManager.Instance.SwordSkill;
            typeMachine = new SwordSkillTypeMachine();

            regularSword = new RegularSwordSkillType(swordSkill, this);
            bounceSword = new BounceSwordSkillType(swordSkill, this);
            pierceSword = new PierceSwordSkillType(swordSkill, this);
            spinSword = new SpinSwordSkillType(swordSkill, this);
        }

        private void Start() { }

        public void Setup(SwordType swordType)
        {
            SetupTypeSword(swordType);
        }

        private void SetupTypeSword(SwordType swordType)
        {
            if (swordType == SwordType.Regular)
                typeMachine.CurrentType = regularSword;
            else if (swordType == SwordType.Bounce)
                typeMachine.CurrentType = bounceSword;
            else if (swordType == SwordType.Pierce)
                typeMachine.CurrentType = pierceSword;
            else if (swordType == SwordType.Spin)
                typeMachine.CurrentType = spinSword;
        }

        private void Update()
        {
            typeMachine.CurrentType?.Update(); 
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (typeMachine.CurrentType.IsReturning) return;
            var enemy = other.GetComponent<Enemy.Enemy>();
            if (enemy != null)
            {
                typeMachine.CurrentType.Damage(enemy);
            }
            
            typeMachine.CurrentType.StuckInto(other);
        }
    }
}