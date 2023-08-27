﻿using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;
using DefaultNamespace;


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
        public Transform target;

        [SerializeField] private GameObject slashPrefab;
        [SerializeField] private GameObject lastSlashPrefab;
        [SerializeField] private GameObject hitPrefab;

        // public Clone CreateClone(Transform cloneTransform, CloneType cloneType, Vector3 offset = default)
        // {
        //     var newClone = Instantiate(clonePrefab);
        //     var newCloneCtr = newClone.GetComponent<Clone>();
        //     newCloneCtr.Setup(cloneTransform, GetCloneSkillTypeBy1(cloneType, newCloneCtr), offset);
        //     return newCloneCtr;
        // }
        //
        // public CloneSkillType GetCloneSkillTypeBy1(CloneType cloneType, Clone cloneCtr)
        // {
        //     return cloneType switch
        //     {
        //         CloneType.Attack => new AttackCloneSkillType(this, cloneCtr, "Attack"),
        //         CloneType.Dash => new DashCloneSkillType(this, cloneCtr, "Dash"),
        //         CloneType.DashAttack => new DashCloneSkillType(this, cloneCtr, "DashAttack"),
        //         _ => new AttackCloneSkillType(this, cloneCtr, "Attack")
        //     };
        // }

        public float CloneDuration => cloneDuration;
        public float DashDuration => dashDuration;
        public float DashSpeed => dashSpeed;

        public float ColorLosingSpeedAttack => colorLosingSpeedAttack;

        public float ColorLosingSpeedDash => colorLosingSpeedDash;
        public Transform Target => target;
        public GameObject SlashPrefab => slashPrefab;
        public GameObject LastSlashPrefab => lastSlashPrefab;
        public GameObject HitPrefab => hitPrefab;
        public GameObject ClonePrefab => clonePrefab;
    }
}