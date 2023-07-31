using System;
using Unity.VisualScripting;
using UnityEngine;

namespace Skill.Sword
{
    public class Sword : MonoBehaviour
    {
        [SerializeField] private float circleCheckRadius;
        public Animator anim { get; private set; }
        public Rigidbody2D rb { get; private set; }
        public CircleCollider2D cd { get; private set; }
        public Player.Player player{ get; private set; }
        private float returnSpeed;
        private float freezeTimeDuration;


        [SerializeField] private LayerMask whatIsLayer;

        public SwordSkillTest swordSkill { get; private set; }
        public SwordSkillTypeMachine typeMachine{ get; private set; }
        private RegularSwordSkillType regularSword;
        private BounceSwordSkillType bounceSword;
        [SerializeField] private Transform circleCheck;

        private void Awake()
        {
            cd = GetComponent<CircleCollider2D>();
            rb = GetComponent<Rigidbody2D>();
            anim = GetComponentInChildren<Animator>();

            swordSkill = SkillManager.Instance.swordSkillTest;
            typeMachine = new SwordSkillTypeMachine();

            regularSword = new RegularSwordSkillType(swordSkill, this);
            bounceSword = new BounceSwordSkillType(swordSkill, this);
        }

        private void Start() { }

        public void Setup(SwordType swordType, float returnSpeed, Player.Player player)
        {
            SetupTypeSword(swordType);
            this.returnSpeed = returnSpeed;
            this.player = player;
        }

        private void SetupTypeSword(SwordType swordType)
        {
            if (swordType == SwordType.Regular)
            {
                typeMachine.CurrentType = regularSword;
            }
            else if (swordType == SwordType.Bounce)
                typeMachine.CurrentType = bounceSword;
        }

        private void Update()
        {
            typeMachine.CurrentType?.Update(); 
        }


        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(circleCheck.position, circleCheckRadius);
        }

        

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (typeMachine.CurrentType.isReturning) return;
            var enemy = other.GetComponent<Enemy.Enemy>();
            if (enemy != null)
            {
                typeMachine.CurrentType.SkillDamage(enemy,freezeTimeDuration);
            }
            
            typeMachine.CurrentType.StuckInto(other);
        }
    }
}