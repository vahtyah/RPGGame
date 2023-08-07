using System;
using Player;
using UnityEngine;

namespace Skill
{
    public abstract class Skill : MonoBehaviour
    {
        [SerializeField] protected float cooldown;
        protected float cooldownTimer;
        protected Player.Player player;
        public event EventHandler onSkillUsed;

        protected virtual void Start() { player = PlayerManager.Instance.player; }

        protected virtual void Update()
        {
            cooldownTimer -= Time.deltaTime;
        }

        public virtual bool CanUseSkill()
        {
            if (!(cooldownTimer < 0))
            {
                Debug.Log("Skill is on cooldown!");
                return false;
            }

            UseSkill(); 
            cooldownTimer = cooldown;
            return true;
        }

        public virtual void UseSkill()
        {
            OnSkillUsed();
        }

        public virtual Transform FindClosestEnemy(Transform checkTransform)
        {
            var colliders = Physics2D.OverlapCircleAll(checkTransform.position, 25);
            var closestDistance = Mathf.Infinity;
            Transform closestTarget = null;
            foreach (var hit in colliders)
            {
                if (hit.GetComponent<Enemy.Enemy>() == null) continue;
                var distanceToTarget = Vector2.Distance(checkTransform.position, hit.transform.position);
                if (!(distanceToTarget < closestDistance)) continue;
                closestDistance = distanceToTarget;
                closestTarget = hit.transform;
            }

            return closestTarget;
        }

        public float Cooldown => cooldown;

        public float CooldownTimer => cooldownTimer;

        protected virtual void OnSkillUsed() { onSkillUsed?.Invoke(this, EventArgs.Empty); }

        public virtual float CooldownNormalized => cooldownTimer / cooldown;
    }
}
