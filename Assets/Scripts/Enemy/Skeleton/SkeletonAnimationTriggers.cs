using UnityEngine;

namespace Enemy.Skeleton
{
    public class SkeletonAnimationTriggers : MonoBehaviour
    {
        private EnemySkeleton enemySkeleton => GetComponentInParent<EnemySkeleton>();

        private void AnimationTrigger() => enemySkeleton.AnimationFinishTrigger();
    }
}