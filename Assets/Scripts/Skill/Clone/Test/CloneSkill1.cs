using System;
using UnityEngine;

namespace Skill.Test
{
    public enum CloneType
    {
        Attack,
        Dash
    }
    public class CloneSkill1 : Skill
    {
        [Header("Clone Info")]
        [SerializeField] private GameObject clonePrefab;
        [SerializeField] private float cloneDuration = 1.5f;

        public void CreateCloneAttack(Transform cloneTransform, Vector3 offset)
        {
            var newClone = Instantiate(clonePrefab);
            var newCloneCtr = newClone.GetComponent<CloneController>();
            newCloneCtr.Setup(cloneTransform, new AttackCloneSkillType(this, newCloneCtr,"Attack"));
        }

        protected override void Update()
        {
            base.Update();
            if (Input.GetKeyDown(KeyCode.L))
            {
                CreateCloneAttack(player.transform, new Vector3(0,0,0));
            }
        }

        public float CloneDuration => cloneDuration;
    }
}