﻿using System;
using UnityEngine;

namespace Skill.Test
{
    public class CloneController : MonoBehaviour
    {
        [SerializeField] private Transform attackCheck;
        [SerializeField] private float attackCheckRadius = .8f;
        public Animator anim { get; private set; }
        public SpriteRenderer sr { get; private set; }
        public Rigidbody2D rb { get; private set; }
        public CloneSkillMachine skillMachine { get; private set; }

        private void Awake()
        {
            skillMachine = new CloneSkillMachine();
            anim = GetComponent<Animator>();
            sr = GetComponent<SpriteRenderer>();
            rb = GetComponent<Rigidbody2D>();
            Debug.Log("Awake");
        }

        private void Start()
        {
            Debug.Log("start");
        }

        public void Setup(Transform newTransform, CloneSkillType skillType)
        {
            Debug.Log("setup");
            transform.position = newTransform.position;
            skillMachine.SkillType = skillType;
        }

        private void Update()
        {
            skillMachine.SkillType?.Update();
        }
        
        private void AnimationTrigger()
        {
            skillMachine.SkillType?.AnimationTrigger();
        }
        
        private void AttackTrigger()
        {
            skillMachine.SkillType?.AttackTrigger();
        }

        public void SelfDestroy()
        {
            Destroy(gameObject);
        }

        public Transform AttackCheck => attackCheck;
        public float AttackCheckRadius => attackCheckRadius;
    }
}