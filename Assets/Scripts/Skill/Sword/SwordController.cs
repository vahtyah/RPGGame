using System;
using Unity.VisualScripting;
using UnityEngine;

namespace Skill.Sword
{
    public class SwordController : MonoBehaviour
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

            swordSkill = SkillManager.Instance.swordSkill;
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
            typeMachine.CurrentType = swordType switch
            {
                SwordType.Regular => regularSword,
                SwordType.Bounce => bounceSword,
                SwordType.Pierce => pierceSword,
                SwordType.Spin => spinSword,
                _ => typeMachine.CurrentType
            };
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