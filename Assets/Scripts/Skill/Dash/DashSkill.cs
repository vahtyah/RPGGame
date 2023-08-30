﻿using System;
using Skill.Test;
using UI;
using Unity.VisualScripting;
using UnityEngine;

namespace Skill
{
    public class DashSkill : Skill
    {
        [Header("Dash")]
        public bool dashUnlocker;

        [SerializeField] private SkillTreeSlotUI dash;

        [Header("Clone on Dash")]
        public bool cloneOnDashUnlocker;

        [SerializeField]
        private SkillTreeSlotUI cloneOnDash;

        [Header("Clone on Dash arrival")]
        public bool cloneOnDashArrivalUnlocker;

        [SerializeField]
        private SkillTreeSlotUI cloneOnDashArrival;

        private void Awake()
        {
            Debug.Log("unlock skill");
            dash.onUnlocked += delegate(object sender, EventArgs args) { UnlockDash(); };
        }

        protected override void Start()
        {
            base.Start();

            // cloneOnDash.onUnlocked += delegate(object sender, EventArgs args) { UnlockCloneOnDash(); };
            // cloneOnDashArrival.onUnlocked += delegate(object sender, EventArgs args) { UnlockCloneOnDashArrival(); };
        }

        public override bool CanUseSkill() { return dashUnlocker && base.CanUseSkill(); }

        private void UnlockDash() => dashUnlocker = dash.unlocker;

        private void UnlockCloneOnDash() =>
            cloneOnDashUnlocker = cloneOnDash.unlocker;

        private void UnlockCloneOnDashArrival() =>
            cloneOnDashArrivalUnlocker = cloneOnDashArrival.unlocker;

        public void DashStartEffect()
        {
            if (cloneOnDashUnlocker)
                Clone.Create(player.transform, CloneType.Attack);
        }

        public void DashFinishEffect()
        {
            if (cloneOnDashArrivalUnlocker)
                Clone.Create(player.transform, CloneType.Attack);
        }
    }
}