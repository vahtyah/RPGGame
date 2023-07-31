using System.Collections.Generic;
using UnityEngine;

namespace Skill.Sword
{
    public class BounceSwordSkillType : SwordSkillType
    {
        private bool isBouncing;
        private List<Transform> enemiesTarget;
        private int targetIndex;
        private int bounceAmount;

        public BounceSwordSkillType(SwordSkillTest swordSkillTest, Sword sword) : base(swordSkillTest, sword)
        {
            this.bounceAmount = swordSkillTest.BounceAmount;
            enemiesTarget = new List<Transform>();
            isBouncing = true;
            targetIndex = 1;
        }

        public override void Setup() { base.Setup(); }

        public override void Update()
        {
            base.Update();
            SKillLogic();
        }

        private void SKillLogic()
        {
            if (isBouncing && enemiesTarget.Count > 0)
            {
                Debug.Log("enemiesTarget.Count = " + enemiesTarget.Count);
                sword.transform.position = Vector2.MoveTowards(sword.transform.position,
                    enemiesTarget[targetIndex].position, swordSkillTest.BounceSpeed * Time.deltaTime);
                if (Vector2.Distance(sword.transform.position, enemiesTarget[targetIndex].position) < .1f)
                {
                    var curEnemy = enemiesTarget[targetIndex].GetComponent<Enemy.Enemy>();
                    SkillDamage(curEnemy, swordSkillTest.freezeTimeDuration);
                    targetIndex++;
                    bounceAmount--;
                    if (bounceAmount <= 0)
                    {
                        isBouncing = false;
                        isReturning = true;
                    }

                    if (targetIndex >= enemiesTarget.Count)
                        targetIndex = 0;
                }
            }
        }

        public override void SkillDamage(Enemy.Enemy enemy, float freezeTimeDuration)
        {
            base.SkillDamage(enemy, freezeTimeDuration);
            SetupTarget();
        }

        public override void StuckInto(Collider2D other)
        {
            Debug.Log("stuck bounce");
            base.StuckInto(other);
            if (isBouncing && enemiesTarget.Count > 0)
            {
                anim.SetBool("Rotation", true);
                sword.transform.parent = null;
            }
        }

        private void SetupTarget()
        {
            if (isBouncing && enemiesTarget.Count <= 0)
            {
                var colliders = Physics2D.OverlapCircleAll(sword.transform.position, swordSkillTest.BounceRadius);
                foreach (var hit in colliders)
                {
                    if (hit.GetComponent<Enemy.Enemy>() != null)
                        enemiesTarget.Add(hit.transform);
                }
            }
        }
    }
}