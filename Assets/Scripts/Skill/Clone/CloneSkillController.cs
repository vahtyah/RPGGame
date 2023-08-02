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
        private bool canDuplicateClone;
        private float facingDir = 1;
        private float chanceToDuplicate;

        private void Awake()
        {
            animator = GetComponent<Animator>();
            sr = GetComponent<SpriteRenderer>();
        }

        public void SetUp(Transform newTransform, float cloneDuration, bool canAttack, Vector3 offset, Transform closestEnemy, bool canDuplicateClone, float chanceToDuplicate)
        {
            if(canAttack) animator.SetInteger("AttackNumber",Random.Range(1,4));
            transform.position = newTransform.position + offset;
            cloneTimer = cloneDuration;
            closestTarget = closestEnemy;
            this.canDuplicateClone = canDuplicateClone;
            this.chanceToDuplicate = chanceToDuplicate;
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
                if (hit.GetComponent<Enemy.Enemy>() != null)
                {
                    hit.GetComponent<Enemy.Enemy>().DamageEffect();
                    if (canDuplicateClone)
                    {
                        if (Random.Range(0, 100) < chanceToDuplicate)
                        {
                            SkillManager.Instance.cloneSkill.CreateClone(hit.transform, new Vector3(.5f * facingDir,0));
                        }
                    }
                }
            }
        }

        private void FaceClosestTarget()
        {
            if (closestTarget == null) return;
            if (transform.position.x > closestTarget.position.x)
            {
                facingDir *= -1;
                transform.Rotate(0,180,0);
            }
        }
    }
}