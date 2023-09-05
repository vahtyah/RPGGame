using System;
using Skill.Test;
using UI;
using UnityEngine;

namespace Skill.Parry
{
    public class ParrySkill : Skill
    {
        [SerializeField] private float parryDuration;
        [SerializeField] private SkillTreeSlotUI parry;
        [SerializeField] private SkillTreeSlotUI cloneOnParry;

        private bool parryUnlocker;
        private bool cloneOnParryUnlocker;
        
        private float parryTimer;

        private void Awake()
        {
            parry.onUnlocked += delegate(object sender, EventArgs args) { UnlockParry(); };
            cloneOnParry.onUnlocked += delegate(object sender, EventArgs args) { UnlockCloneOnParry(); };
        }
        
        public override bool UseSkill() { return parryUnlocker && base.UseSkill(); }


        protected override void StartSkill()
        {
            base.StartSkill();
            parryTimer = parryDuration;
            player.stateMachine.State = player.counterAttackState;
        }

        public override void CompleteSkill()
        {
            base.CompleteSkill();
            player.stateMachine.State = player.idleState;
        }

        protected override void Logic()
        {
            base.Logic();
            parryTimer -= Time.deltaTime;
            var colliders = Physics2D.OverlapCircleAll(player.attackCheck.position, player.attackCheckRadius);
            foreach (var hit in colliders)
            {
                if(hit.GetComponent<Enemy.Enemy>() != null)
                    if (hit.GetComponent<Enemy.Enemy>().CanBeStunned())
                    {
                        parryTimer = 10;
                        player.anim.SetBool("SuccessfulAttack",true);
                        if (cloneOnParryUnlocker)
                        {
                            // canCreateClone = false;
                            Clone.Create(hit.transform, CloneType.Attack, new Vector3(3f * player.facingDir, 0));
                        }
                    }
            }

            if (parryTimer < 0)
                CompleteSkill();
        }

        private void UnlockParry() => parryUnlocker = parry.unlocker;
        private void UnlockCloneOnParry() => cloneOnParryUnlocker = cloneOnParry.unlocker;

    }
}