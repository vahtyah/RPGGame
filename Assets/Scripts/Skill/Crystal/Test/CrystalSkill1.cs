using System;
using UnityEngine;

namespace Skill.Crystal.Test
{
    public class CrystalSkill1 : Skill
    {
        [SerializeField] private GameObject crystalPrefab;

        #region Type

        public MultiCrystalSkill multiCrystal { get; private set; }

        #endregion

        private void Awake()
        {
        }

        public override void StartSkill()
        {
            base.StartSkill();
        }
    }
}