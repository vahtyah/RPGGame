using UnityEngine;

namespace Skill.Sword
{
    public class SpinSwordSkillType : SwordSkillType
    {
        private bool isSpinning;
        private bool wasStopped;
        private float spinTimer;
        private float spinDir;
        private float hitTimer;
        public SpinSwordSkillType(SwordSkill swordSkill, Sword sword) : base(swordSkill, sword)
        {
            isSpinning = true;
        }

        public override void Setup()
        {
            base.Setup();
            anim.SetBool("Rotation", true);
            spinDir = Mathf.Clamp(rb.velocity.x, -1, 1);
        }

        public override void Update()
        {
            base.Update(); 
            Logic();
        }

        private void Logic()
        {
            if (isSpinning)
            {
                if (Vector2.Distance(player.transform.position, sword.transform.position) > swordSkill.MaxTravelDistance && !wasStopped)
                {
                    StopWhenSpinning();
                }

                if (wasStopped)
                {
                    spinTimer -= Time.deltaTime;
                    var position = sword.transform.position;
                    position = Vector2.MoveTowards(position,
                        new Vector2(position.x + spinDir, position.y * spinDir),
                        1.5f * Time.deltaTime);
                    sword.transform.position = position;

                    if (spinTimer < 0)
                    {
                        isReturning = true;
                        isSpinning = false;
                    }

                    hitTimer -= Time.deltaTime;
                    if (hitTimer < 0)
                    {
                        hitTimer = swordSkill.HitCooldown;
                        var colliders = Physics2D.OverlapCircleAll(sword.transform.position, 1);
                        foreach (var hit in colliders)
                        {
                            if (hit.GetComponent<Enemy.Enemy>() != null)
                            {
                                Damage(hit.GetComponent<Enemy.Enemy>());
                            }
                        }
                    }
                }
            }
        }

        public override void StuckInto(Collider2D other)
        {
            if(!wasStopped)
                StopWhenSpinning();
        }

        private void StopWhenSpinning()
        {
            wasStopped = true;
            rb.constraints = RigidbodyConstraints2D.FreezePosition;
            spinTimer = swordSkill.SpinDuration;
        }
    }
}