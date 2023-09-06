using System;
using System.Collections;
using UnityEngine;

namespace Skill.Test
{
    public class Clone : MonoBehaviour
    {
        public static Clone Create(Transform cloneTransform, CloneType cloneType, Vector3 offset = default,
            Transform target = default)
        {
            var newClone = Instantiate(SkillManager.Instance.cloneSkill1.ClonePrefab);
            var newCloneCtr = newClone.GetComponent<Clone>();
            newCloneCtr.Setup(cloneTransform, cloneType, target, offset);
            return newCloneCtr;
        }

        public static IEnumerator Create(Transform cloneTransform, Vector3 offset, float delay)
        {
            yield return new WaitForSeconds(delay);
            var newClone = Instantiate(SkillManager.Instance.cloneSkill1.ClonePrefab);
            var newCloneCtr = newClone.GetComponent<Clone>();
            newCloneCtr.Setup(cloneTransform, CloneType.Attack, default, offset);
        }

        private CloneSkillType GetCloneSkillTypeBy(CloneType cloneType)
        {
            var cloneSkill = SkillManager.Instance.cloneSkill1;
            return cloneType switch
            {
                CloneType.Attack => new AttackCloneSkillType(cloneSkill, this, "Attack"),
                CloneType.Dash => new DashCloneSkillType(cloneSkill, this, "Dash"),
                CloneType.DashAttack => new DashCloneSkillType(cloneSkill, this, "DashAttack"),
                _ => new AttackCloneSkillType(cloneSkill, this, "Attack")
            };
        }

        [SerializeField] private Transform attackCheck;
        [SerializeField] private float attackCheckRadius = .8f;
        public CloneType cloneType { get; private set; }
        public Transform target { get; private set; }
        public Animator anim { get; private set; }
        public SpriteRenderer sr { get; private set; }
        public Rigidbody2D rb { get; private set; }
        public CloneSkillMachine skillMachine { get; private set; }

        private void Awake()
        {
            skillMachine = new CloneSkillMachine();
            anim = GetComponent<Animator>();
            sr = GetComponent<SpriteRenderer>();
            rb = GetComponent<Rigidbody2D>();
        }

        private void Setup(Transform newTransform, CloneType cloneType, Transform target, Vector3 offset)
        {
            transform.position = newTransform.position + offset;
            this.target = target == default ? newTransform : target;
            skillMachine.SkillType = GetCloneSkillTypeBy(cloneType);
            this.cloneType = cloneType;
        }

        private void Update()
        {
            skillMachine.SkillType?.Update();
        }

        private void AnimationTrigger()
        {
            skillMachine.SkillType?.AnimationTrigger();
        }

        private void AttackTrigger()
        {
            skillMachine.SkillType?.AttackTrigger();
        }

        public void SelfDestroy()
        {
            Destroy(gameObject);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            skillMachine.SkillType?.OnTriggerEnter(other);
        }

        public Transform AttackCheck => attackCheck;
        public float AttackCheckRadius => attackCheckRadius;
    }
}