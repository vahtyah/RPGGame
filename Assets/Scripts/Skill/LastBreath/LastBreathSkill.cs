using System.Collections;
using UnityEngine;

namespace Skill.LastBreath
{
    public class LastBreathSkill : Skill
    {
        public Transform target { get; private set; }

        public override void UseSkill()
        {
            base.UseSkill();
            
        }

        private IEnumerator ProgressUseSkill()
        {
            yield break;
        }
    }
}