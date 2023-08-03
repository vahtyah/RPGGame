using System;
using System.Collections.Generic;
using UnityEngine;
using Player;

namespace Skill
{
    public class SwordSkillController : MonoBehaviour
    {
        private Animator anim;
        private Rigidbody2D rb;
        private CircleCollider2D cd;
        private Player.Player player;
        private bool isReturnSword;
        private float returnSpeed;

        private bool canRotate = true;

        private float freezeTimeDuration;

        [Header("Bounce Info")]
        private float bounceSpeed = 20f;

        private bool isBouncing;
        private int bouncesAmount;
        private List<Transform> enemiesTarget;
        private int targetIndex;

        [Header("Pierce Info")]
        [SerializeField] private int pierceAmount;

        [Header("Spin Info")] private float maxTravelDistance;
        private float spinDuration;
        private float spinTimer;
        private bool wasStopped;
        private bool isSpinning;

        private float hitTimer;
        private float hitCooldown;

        private float spinDir;

        private void Awake()
        {
            cd = GetComponent<CircleCollider2D>();
            rb = GetComponent<Rigidbody2D>();
            anim = GetComponentInChildren<Animator>();
        }

        private void DestroyMe()
        {
            Destroy(gameObject);
        }

        public void Setup(Vector2 dir, float gravityScale, Player.Player player, float freezeTimeDuration, float returnSpeed)
        {
            this.player = player;
            rb.gravityScale = gravityScale;
            rb.velocity = dir;
            this.returnSpeed = returnSpeed;
            enemiesTarget = new List<Transform>();
            this.freezeTimeDuration = freezeTimeDuration;
            if (pierceAmount <= 0)
                anim.SetBool("Rotation", true);

            spinDir = Mathf.Clamp(rb.velocity.x, -1, 1);
            Invoke("DestroyMe",7);
        }

        public void SetupBounce(bool isBouncing, int amountOfBounces, float bounceSpeed)
        {
            this.isBouncing = isBouncing;
            this.bouncesAmount = amountOfBounces;
            this.bounceSpeed = bounceSpeed;
        }

        public void SetupPierce(int pierceAmount) { this.pierceAmount = pierceAmount; }

        public void SetupSpin(bool isSpinning, float maxTravelDistance, float spinDuration, float hitCooldown)
        {
            this.isSpinning = isSpinning;
            this.maxTravelDistance = maxTravelDistance;
            this.spinDuration = spinDuration;
            this.hitCooldown = hitCooldown;
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
            if (canRotate)
                transform.right = rb.velocity;

            if (isReturnSword)
            {
                transform.position = Vector2.MoveTowards(transform.position, player.transform.position,
                    returnSpeed * Time.deltaTime);
                if (Vector2.Distance(transform.position, player.transform.position) < .5f)
                    player.CatchTheSword();
            }

            BounceLogic();

            SpinLogic();
        }

        private void SpinLogic()
        {
            if (isSpinning)
            {
                if (Vector2.Distance(player.transform.position, transform.position) > maxTravelDistance && !wasStopped)
                {
                    StopWhenSpinning();
                }

                if (wasStopped)
                {
                    spinTimer -= Time.deltaTime;
                    transform.position = Vector2.MoveTowards(transform.position,
                        new Vector2(transform.position.x + spinDir, transform.position.y + spinDir),
                        1.5f * Time.deltaTime);
                    if (spinTimer < 0)
                    {
                        isReturnSword = true;
                        isSpinning = false;
                    }

                    hitTimer -= Time.deltaTime;
                    if (hitTimer < 0)
                    {
                        hitTimer = hitCooldown;
                        var colliders = Physics2D.OverlapCircleAll(transform.position, 1);
                        foreach (var hit in colliders)
                        {
                            if (hit.GetComponent<Enemy.Enemy>() != null)
                            {
                                SwordSkillDamage(hit.GetComponent<Enemy.Enemy>());
                            }
                        }
                    }
                }
            }
        }

        private void StopWhenSpinning()
        {
            wasStopped = true;
            rb.constraints = RigidbodyConstraints2D.FreezePosition;
            spinTimer = spinDuration;
        }

        private void BounceLogic()
        {
            if (isBouncing && enemiesTarget.Count > 0)
            {
                transform.position = Vector2.MoveTowards(transform.position,
                    enemiesTarget[targetIndex].position, bounceSpeed * Time.deltaTime);
                if (Vector2.Distance(transform.position, enemiesTarget[targetIndex].position) < .1f)
                {
                    var enemy = enemiesTarget[targetIndex].GetComponent<Enemy.Enemy>();
                    SwordSkillDamage(enemy);
                    targetIndex++;
                    bouncesAmount--;
                    if (bouncesAmount <= 0)
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
            var enemy = other.GetComponent<Enemy.Enemy>();
            if (enemy != null)
            {
                SwordSkillDamage(enemy);
                SetupTargesForBounce(enemy);
            }

            StuckInto(other);
        }

        private void SwordSkillDamage(Enemy.Enemy enemy)
        {
            enemy.DamageImpact();
            enemy.StartCoroutine("FreezeTimerFor", freezeTimeDuration);
        }

        private void SetupTargesForBounce(Enemy.Enemy enemy)
        {
            if (isBouncing && enemiesTarget.Count <= 0)
            {
                var colliders = Physics2D.OverlapCircleAll(transform.position, 10);
                foreach (var hit in colliders)
                {
                    if (hit.GetComponent<Enemy.Enemy>() != null)
                        enemiesTarget.Add(hit.transform);
                }
            }
        }

        private void StuckInto(Collider2D other)
        {
            if (pierceAmount > 0 && other.GetComponent<Enemy.Enemy>() != null)
            {
                pierceAmount--;
                return;
            }

            if (isSpinning)
            {
                StopWhenSpinning();
                return;
            }

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