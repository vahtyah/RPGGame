using System;
using UnityEditor.PackageManager;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Skill
{
    public class CloneSkillController : MonoBehaviour
    {
        [SerializeField] private float colorLosingSpeed;
        [SerializeField] private Transform attackCheck;
        [SerializeField] private float attackCheckRadius = .8f;
        private SpriteRenderer sr;
        private Animator animator;
        private float cloneTimer;
        private Transform closestTarget;

        private void Awake()
        {
            animator = GetComponent<Animator>();
            sr = GetComponent<SpriteRenderer>();
        }

        public void SetUp(Transform newTransform, float cloneDuration, bool canAttack)
        {
            if(canAttack) animator.SetInteger("AttackNumber",Random.Range(1,4));
            transform.position = newTransform.position;
            cloneTimer = cloneDuration;

            FaceClosestTarget();
        }

        private void Update()
        {
            cloneTimer -= Time.deltaTime;
            if (cloneTimer < 0)
                sr.color = new Color(1, 1, 1, sr.color.a - (Time.deltaTime * colorLosingSpeed));
            if(sr.color.a <= 0)
                Destroy(gameObject);
        }
        
        private void AnimationTrigger()
        {
            cloneTimer = -.1f;
        }

        private void AttackTrigger()
        {
            var colliders = Physics2D.OverlapCircleAll(attackCheck.position, attackCheckRadius);
            foreach (var hit in colliders)
            {
                if (hit.GetComponent<Enemy.Enemy>() != null) hit.GetComponent<Enemy.Enemy>().Damage();
            }
        }

        private void FaceClosestTarget()
        {
            var colliders = Physics2D.OverlapCircleAll(transform.position, 25);
            var closestDistance = Mathf.Infinity;
            foreach (var hit in colliders)
            {
                if (hit.GetComponent<Enemy.Enemy>() == null) continue;
                var distanceToTarget = Vector2.Distance(transform.position, hit.transform.position);
                if (!(distanceToTarget < closestDistance)) continue;
                closestDistance = distanceToTarget;
                closestTarget = hit.transform;
            }

            if (closestTarget == null) return;
            if (transform.position.x > closestTarget.position.x) transform.Rotate(0,180,0);
        }
    }
}