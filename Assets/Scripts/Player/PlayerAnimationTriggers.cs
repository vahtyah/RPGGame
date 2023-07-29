using Skill;
using UnityEngine;

namespace Player
{
    public class PlayerAnimationTriggers : MonoBehaviour
    {
        private Player player;

        private void Start()
        {
            player = GetComponentInParent<Player>();
        }

        private void AnimationTrigger()
        {
            player.AnimationTrigger();
        }

        private void AttackTrigger()
        {
            var colliders = Physics2D.OverlapCircleAll(player.attackCheck.position, player.attackCheckRadius);
            foreach (var hit in colliders)
            {
                if (hit.GetComponent<Enemy.Enemy>() != null) hit.GetComponent<Enemy.Enemy>().Damage();
            }
        }

        private void ThrowSword()
        {
            SkillManager.Instance.swordSkill.CreateSword();
        }
    }
}