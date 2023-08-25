using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Skill.Test
{
    public enum CloneType
    {
        Attack,
        Dash,
        DashAttack
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

        [Header("Clone Dash Attack")]
        [SerializeField] private Transform target;

        public void CreateClone(Transform cloneTransform, CloneType cloneType, Vector3 offset = default)
        {
            var newClone = Instantiate(clonePrefab);
            var newCloneCtr = newClone.GetComponent<CloneController>();
            newCloneCtr.Setup(cloneTransform, GetCloneSkillTypeBy(cloneType, newCloneCtr), offset);
        }

        public CloneSkillType GetCloneSkillTypeBy(CloneType cloneType, CloneController cloneCtr)
        {
            return cloneType switch
            {
                CloneType.Attack => new AttackCloneSkillType(this, cloneCtr, "Attack"),
                CloneType.Dash => new DashCloneSkillType(this, cloneCtr, "Dash"),
                CloneType.DashAttack => new DashCloneSkillType(this, cloneCtr, "DashAttack"),
                _ => new AttackCloneSkillType(this, cloneCtr, "Attack")
            };
        }

        protected override void Update()
        {
            base.Update();
            if (Input.GetKeyDown(KeyCode.L))
            {
                Vector3[] randomVector3 =
                {
                    new(1.5f, 0), new(-1.5f, 0), new(1.5f, -1.5f), new(-1.5f, -1.5f),
                };
                int randomIndex = Random.Range(0, randomVector3.Length);
                Vector3 randomVector = randomVector3[randomIndex];
                CreateClone(target, CloneType.Dash, randomVector);
            }
        }

        public float CloneDuration => cloneDuration;
        public float DashDuration => dashDuration;
        public float DashSpeed => dashSpeed;

        public float ColorLosingSpeedAttack => colorLosingSpeedAttack;

        public float ColorLosingSpeedDash => colorLosingSpeedDash;
        public Transform Target => target;
    }
}