using System;
using UnityEngine;

namespace Skill
{
    public class CloneSkillController : MonoBehaviour
    {
        private SpriteRenderer sr;
        [SerializeField] private float colorLosingSpeed;
        private float cloneTimer;

        private void Awake() { sr = GetComponent<SpriteRenderer>(); }

        public void SetUp(Transform newTransform, float cloneDuration)
        {
            transform.position = newTransform.position;
            cloneTimer = cloneDuration;
        }

        private void Update()
        {
            cloneTimer -= Time.deltaTime;
            if (cloneTimer < 0)
            {
                sr.color = new Color(1, 1, 1, sr.color.a - (Time.deltaTime * colorLosingSpeed));
            }
        }
    }
}