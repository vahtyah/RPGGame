using System.Collections;
using Player;
using Skill.Test;
using UnityEngine;

namespace Skill.LastBreath
{
    public class LastBreathSkill : Skill
    {
        [SerializeField] private int numberOfSlashed = 5;
        private bool isUsedSkill;

        private Vector3[] randomVector3 =
        {
            new(1.5f, 0), new(-1.5f, 0), new(1.5f, -1.5f), new(-1.5f, -1.5f),
        };

        public Transform target;

        public Transform Target
        {
            get => target;
            set => target = value;
        }

        protected override void Start()
        {
            base.Start();
        }

        public override void UseSkill()
        {
            base.UseSkill();
            player.stateMachine.State = player.holdTornadoState;
        }
        
        public void use()
        {
            if (target)
            {
                StartCoroutine(ProgressUseSkill());
            }
        }

        protected override void Update()
        {
            base.Update();
        }

        private IEnumerator ProgressUseSkill()
        {
            var enemyScript = target.GetComponent<Enemy.Enemy>();
            var lastIndex = 0;
            var num = numberOfSlashed;
            player.stateMachine.State = player.lastBreathSkillState;
            enemyScript.stateMachine.State = enemyScript.airState; //TODO: fix
            Clone.Create(player.transform, CloneType.Dash);
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

                Clone.Create(target, CloneType.DashAttack, randomVector);
                yield return new WaitForSeconds(.2f);
            }

            player.transform.position = target.position + new Vector3(1.5f, .6f);
            player.stateMachine.State = new PlayerPrimaryAttackState(player.stateMachine, player, "Attack", 2);
            player.FlipController(-1);
            Slash.Create(SkillManager.Instance.cloneSkill1.LastSlashPrefab, target.position, default);
            Hit.Create(SkillManager.Instance.cloneSkill1.HitPrefab, target.position, Vector3.down);

            yield return new WaitForSeconds(.5f);
            player.stateMachine.State = player.idleState;
            enemyScript.stateMachine.State = enemyScript.idleState;

            target = null;
        }
    }
}