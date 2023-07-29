using UnityEngine;
namespace Enemy.Skeleton
{
    public class SkeletonAnimationTriggers : MonoBehaviour
    {
        private EnemySkeleton enemySkeleton => GetComponentInParent<EnemySkeleton>();

        private void AnimationTrigger() => enemySkeleton.AnimationFinishTrigger();
        private void AttackTrigger()
        {
            var colliders =
                Physics2D.OverlapCircleAll(enemySkeleton.attackCheck.position, enemySkeleton.attackCheckRadius);
            foreach (var hit in colliders)
            {
                if(hit.GetComponent<Player.Player>() != null) hit.GetComponent<Player.Player>().Damage();
            }
        }

        private void OpenCounterWindow() => enemySkeleton.OpenCounterAttackWindow();
        private void CloseCounterWindow() => enemySkeleton.CloseCounterAttackWindow();
    }
}