﻿using System;
using Player;
using UnityEngine;

namespace Skill
{
    public abstract class Skill : MonoBehaviour
    {
        [SerializeField] protected float cooldown;
        protected float cooldownTimer;
        protected Player.Player player;

        protected virtual void Start() { player = PlayerManager.Instance.player; }

        protected virtual void Update()
        {
            cooldownTimer -= Time.deltaTime;
        }

        public virtual bool CanUseSkill()
        {
            if (!(cooldownTimer < 0)) return false;
            UseSkill();
            cooldownTimer = cooldown;
            return true;

        }

        public virtual void UseSkill()
        {
            //skill use
        }
    }
}
