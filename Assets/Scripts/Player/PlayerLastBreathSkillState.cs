using System.Collections;
using Enemy.Skeleton;
using Skill;
using Skill.Test;
using UnityEngine;

namespace Player
{
    public class PlayerLastBreathSkillState : PlayerState
    {
        private Vector3[] randomVector3 =
        {
            new(1.5f, 0), new(-1.5f, 0), new(1.5f, -1.5f), new(-1.5f, -1.5f),
        };

        private CloneSkill1 cloneSkill;
        
        public PlayerLastBreathSkillState(PlayerStateMachine stateMachine, Player player, string animBoolName) : base(stateMachine, player, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            player.fx.MakeTransparent(true);
            cloneSkill = SkillManager.Instance.cloneSkill1;
        }

        public override void Update()
        {
            base.Update();
            player.SetZeroVelocity();
        }


        public IEnumerator EffectSkill()
        {
            var num = 5;

            var enemyScript = cloneSkill.target.GetComponent<Enemy.Enemy>();
            enemyScript.FreezeTimer(true);
            enemyScript.stateMachine.State = (enemyScript as EnemySkeleton)!.airState; //TODO: fix
            cloneSkill.CreateClone(player.transform, CloneType.Dash);
            player.transform.position = cloneSkill.target.transform.position;
            
            yield return new WaitForSeconds(.5f);
            
            while (num > 0)
            {
                num--;
                int randomIndex = Random.Range(0, randomVector3.Length);
                Vector3 randomVector = randomVector3[randomIndex];
                cloneSkill.CreateClone(cloneSkill.target, CloneType.DashAttack, randomVector);
                yield return new WaitForSeconds(.2f);
            }

            // var lastClone = cloneSkill.CreateClone(cloneSkill.target, CloneType.Attack, new Vector3(1, .6f));
            // lastClone.anim.SetInteger("AttackNumber",3);

            player.transform.position = cloneSkill.target.transform.position + new Vector3(1.5f, .6f);
            player.stateMachine.State = new PlayerPrimaryAttackState(player.stateMachine, player, "Attack", 2);
            player.FlipController(-1);
            Slash.Create(cloneSkill.LastSlashPrefab, cloneSkill.target.transform.position, default);
            Hit.Create(cloneSkill.HitPrefab, cloneSkill.target.transform.position, Vector3.down);
            
            cloneSkill.target.GetComponent<Enemy.Enemy>().FreezeTimer(false);
            yield return new WaitForSeconds(.5f);
            player.stateMachine.State = player.idleState;
            enemyScript.stateMachine.State = (enemyScript as EnemySkeleton)!.idleState; //TODO: fix
        }

        public override void Exit()
        {
            base.Exit(); 
            player.fx.MakeTransparent(false);
        }
    }
}