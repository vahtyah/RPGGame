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

        public BounceSwordSkillType(SwordSkill swordSkill, Sword sword) : base(swordSkill, sword)
        {
            bounceAmount = swordSkill.BounceAmount;
            enemiesTarget = new List<Transform>();
            isBouncing = true;
            targetIndex = 1;
        }

        public override void Setup()
        {
            base.Setup(); 
            anim.SetBool("Rotation", true);
        }

        public override void Update()
        {
            base.Update();
            Logic();
        }

        private void Logic()
        {
            if (isBouncing && enemiesTarget.Count > 0)
            {
                sword.transform.position = Vector2.MoveTowards(sword.transform.position,
                    enemiesTarget[targetIndex].position, swordSkill.BounceSpeed * Time.deltaTime);
                if (Vector2.Distance(sword.transform.position, enemiesTarget[targetIndex].position) < .1f)
                {
                    var curEnemy = enemiesTarget[targetIndex].GetComponent<Enemy.Enemy>();
                    Damage(curEnemy);
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

        public override void Damage(Enemy.Enemy enemy)
        {
            base.Damage(enemy);
            SetupTarget();
        }

        public override void StuckInto(Collider2D other)
        {
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
                var colliders = Physics2D.OverlapCircleAll(sword.transform.position, swordSkill.BounceRadius);
                foreach (var hit in colliders)
                {
                    if (hit.GetComponent<Enemy.Enemy>() != null)
                        enemiesTarget.Add(hit.transform);
                }
            }
        }
    }
}