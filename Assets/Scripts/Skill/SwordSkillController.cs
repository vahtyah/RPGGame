using System;
using System.Collections.Generic;
using UnityEngine;
using Player;
namespace Skill
{
    public class SwordSkillController : MonoBehaviour
    {
        [SerializeField] private float returnSpeed;
        private Animator anim;
        private Rigidbody2D rb;
        private CircleCollider2D cd;
        private Player.Player player;
        private bool isReturnSword;

        private bool canRotate = true;

        [Header("Bounce Info")]
        [SerializeField] private float bounceSpeed = 20f;
        private bool isBouncing;
        private int amountOfBounces;
        private List<Transform> enemiesTarget;
        private int targetIndex;


        private void Awake()
        {
            cd = GetComponent<CircleCollider2D>();
            rb = GetComponent<Rigidbody2D>();
            anim = GetComponentInChildren<Animator>();
        }

        public void Setup(Vector2 dir, float gravityScale, Player.Player player)
        {
            this.player = player;
            rb.gravityScale = gravityScale;
            rb.velocity = dir;
            enemiesTarget = new List<Transform>();

            anim.SetBool("Rotation",true);
        }

        public void SetupBounce(bool isBouncing, int amountOfBounces)
        {
            this.isBouncing = isBouncing;
            this.amountOfBounces = amountOfBounces;
        }

        public void ReturnSword()
        {
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            // rb.isKinematic = false;
            transform.parent = null;
            isReturnSword = true;
        }

        private void Update()
        {
            if(canRotate)
                transform.right = rb.velocity;

            if (isReturnSword)
            {
                transform.position = Vector2.MoveTowards(transform.position, player.transform.position,
                    returnSpeed * Time.deltaTime);
                if(Vector2.Distance(transform.position, player.transform.position) < .5f)
                    player.CatchTheSword();
            }

            BounceLogic();
        }

        private void BounceLogic()
        {
            if (isBouncing && enemiesTarget.Count > 0)
            {
                transform.position = Vector2.MoveTowards(transform.position,
                    enemiesTarget[targetIndex].position, bounceSpeed * Time.deltaTime);
                if (Vector2.Distance(transform.position, enemiesTarget[targetIndex].position) < .1f)
                {
                    targetIndex++;
                    amountOfBounces--;
                    if (amountOfBounces <= 0)
                    {
                        isBouncing = false;
                        isReturnSword = true;
                    }

                    if (targetIndex >= enemiesTarget.Count)
                        targetIndex = 0;
                }
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (isReturnSword) return;

            if (other.GetComponent<Enemy.Enemy>() != null)
            {
                if (isBouncing && enemiesTarget.Count <= 0)
                {
                    var colliders = Physics2D.OverlapCircleAll(transform.position, 10);
                    foreach (var hit in colliders)
                    {
                        if(hit.GetComponent<Enemy.Enemy>() != null)
                            enemiesTarget.Add(hit.transform);
                    }
                }
            }
            
            StuckInto(other);
        }

        private void StuckInto(Collider2D other)
        {
            canRotate = false;
            cd.enabled = false;
            rb.isKinematic = true;
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            if (isBouncing && enemiesTarget.Count > 0)
                return; 
            anim.SetBool("Rotation", false);
            transform.parent = other.transform;
        }
    }
}