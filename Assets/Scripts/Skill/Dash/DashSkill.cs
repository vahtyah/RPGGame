using System;
using Skill.Clone;
using Skill.Test;
using UI;
using Unity.VisualScripting;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

namespace Skill
{
    public class DashSkill : Skill
    {
        [Header("Dash")] public bool dashUnlocker;
        private float dashDir;

        [SerializeField] private SkillTreeSlotUI dash;

        [Header("Clone on Dash")] public bool cloneOnDashUnlocker;

        [SerializeField] private SkillTreeSlotUI cloneOnDash;

        [Header("Clone on Dash arrival")] public bool cloneOnDashArrivalUnlocker;

        [SerializeField] private SkillTreeSlotUI cloneOnDashArrival;

        private void Awake()
        {
            dash.onUnlocked += delegate(object sender, EventArgs args) { UnlockDash(); };
            cloneOnDash.onUnlocked += delegate(object sender, EventArgs args) { UnlockCloneOnDash(); };
            cloneOnDashArrival.onUnlocked += delegate(object sender, EventArgs args) { UnlockCloneOnDashArrival(); };
        }

        public override bool UseSkill() 
        {
            return dashUnlocker && !player.IsWallDetected() && base.UseSkill();
        }

        public override void StartSkill()
        {
            base.StartSkill();
            dashDir = Input.GetAxisRaw("Horizontal");
            if(dashDir == 0) dashDir = player.GetFacingDirection;
            player.stateMachine.State = player.dashState;
        }

        private void UnlockDash() => dashUnlocker = dash.unlocker;

        private void UnlockCloneOnDash() =>
            cloneOnDashUnlocker = cloneOnDash.unlocker;

        private void UnlockCloneOnDashArrival() =>
            cloneOnDashArrivalUnlocker = cloneOnDashArrival.unlocker;

        public void DashStartEffect()
        {
            if (cloneOnDashUnlocker)
                Clone.Clone.Create(player.transform, CloneType.Attack);
        }

        public void DashFinishEffect()
        {
            if (cloneOnDashArrivalUnlocker)
                Clone.Clone.Create(player.transform, CloneType.Attack);
        }
        
        public float GetFacingDirection => dashDir;

    }
}