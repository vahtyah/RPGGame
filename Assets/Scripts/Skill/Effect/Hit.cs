using UnityEngine;

namespace Skill.Test
{
    public class Hit : Effect
    {
        public static Hit Create(GameObject prefab, Vector3 position, Vector3 dir)
        {
            var hitOj = Instantiate(prefab, position, Quaternion.identity);
            hitOj.transform.right = dir;
            var hitScript = hitOj.GetComponent<Hit>();
            return hitScript;
        }
        
        private void AnimationTrigger()
        {
            Destroy(gameObject);
        }
    }
}