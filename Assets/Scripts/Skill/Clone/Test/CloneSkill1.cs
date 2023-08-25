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
        
        [Header("Clone Attack")]
        [SerializeField] private float colorLosingSpeedAttack = 1;
        
        [Header("Clone Dash")]
        [SerializeField] private float dashDuration = .2f;
        [SerializeField] private float dashSpeed = 25;
        [SerializeField] private float colorLosingSpeedDash = 1;

        public void CreateClone(Transform cloneTransform, CloneType cloneType, Vector3 offset)
        {
            var newClone = Instantiate(clonePrefab);
            var newCloneCtr = newClone.GetComponent<CloneController>();
            newCloneCtr.Setup(cloneTransform, GetCloneSkillTypeBy(cloneType, newCloneCtr));
        }

        public CloneSkillType GetCloneSkillTypeBy(CloneType cloneType, CloneController cloneCtr)
        {
            return cloneType switch
            {
                CloneType.Attack => new AttackCloneSkillType(this, cloneCtr, "Attack"),
                CloneType.Dash => new DashCloneSkillType(this, cloneCtr, "Dash"),
                _ => new AttackCloneSkillType(this, cloneCtr, "Attack")
            };
        }

        protected override void Update()
        {
            base.Update();
            if (Input.GetKeyDown(KeyCode.L))
            {
                CreateClone(player.transform, CloneType.Dash, Vector3.zero);
            }
        }
        
        

        public float CloneDuration => cloneDuration;
        public float DashDuration => dashDuration;
        public float DashSpeed => dashSpeed;

        public float ColorLosingSpeedAttack => colorLosingSpeedAttack;

        public float ColorLosingSpeedDash => colorLosingSpeedDash;
    }
}