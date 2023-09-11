using System;
using System.Collections;
using Player;
using Skill.Clone;
using Skill.Test;
using UI;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Skill.LastBreath
{
    public class LastBreathSkill : Skill
    {
        [SerializeField] private int numberOfSlashed = 5;
        [SerializeField] private SkillTreeSlotUI lastBreathSkillTreeSlot;
        private bool lastBreathUnlocker;
        
        private bool isUsedSkill;

        private Vector3[] randomVector3 =
        {
            new(1.5f, 0), new(-1.5f, 0), new(1.5f, -1.5f), new(-1.5f, -1.5f),
        };

        public Transform target { get; private set; }

        public Transform Target
        {
            get => target;
            set => target = value;
        }

        private void Awake()
        {
            lastBreathSkillTreeSlot.onUnlocked += delegate(object sender, EventArgs args)
            {
                UnlockerLastBreath();
            };
        }

        private void UnlockerLastBreath()
        {
            lastBreathUnlocker = lastBreathSkillTreeSlot.unlocker;
        }

        public override bool UseSkill()
        {
            return lastBreathUnlocker && base.UseSkill();
        }

        public override void StartSkill()
        {
            base.StartSkill();
            player.stateMachine.State = player.holdTornadoState;
        }

        public void use()
        {
            if (target)
            {
                StartCoroutine(ProgressUseSkill());
            }
        }

        protected override void Update() { base.Update(); }

        private IEnumerator ProgressUseSkill()
        {
            var enemyScript = target.GetComponent<Enemy.Enemy>();
            var lastIndex = 0;
            var num = numberOfSlashed;
            player.stateMachine.State = player.lastBreathSkillState;
            enemyScript.stateMachine.State = enemyScript.airState; //TODO: fix
            Clone.Clone.Create(player.transform, CloneType.Dash, Vector3.zero, target);
            player.transform.position = target.position;
            yield return new WaitForSeconds(.5f);

            while (num > 0)
            {
                num--;
                var randomIndex = Random.Range(0, randomVector3.Length);

                while (randomIndex == lastIndex)
                {
                    randomIndex = Random.Range(0, randomVector3.Length);
                }

                lastIndex = randomIndex;

                var randomVector = randomVector3[randomIndex];

                Clone.Clone.Create(target, CloneType.DashAttack, randomVector);
                yield return new WaitForSeconds(.2f);
            }

            player.transform.position = target.position + new Vector3(1.5f, .6f);
            player.stateMachine.State = new PlayerPrimaryAttackState(player.stateMachine, player, "Attack", 2);
            player.FlipController(-1);
            Slash.Create(SkillManager.Instance.cloneSkill.LastSlashPrefab, target.position, default);
            Hit.Create(SkillManager.Instance.cloneSkill.HitPrefab, target.position, Vector3.down);

            yield return new WaitForSeconds(.5f);
            player.stateMachine.State = player.idleState;
            enemyScript.stateMachine.State = enemyScript.idleState;

            target = null;
        }
    }
}