﻿using UnityEngine;

namespace Skill
{
    public class CloneSkill : Skill
    {
        [Header("Clone Info")]
        [SerializeField] private GameObject clonePrefab;
        [SerializeField] private float cloneDuration;
        [Space] 
        [SerializeField] private bool canAttack;

        public void CreateClone(Transform cloneTransform)
        {
            var newClone = Instantiate(clonePrefab);
            newClone.GetComponent<CloneSkillController>().SetUp(cloneTransform, cloneDuration, canAttack);
        }
    }
}