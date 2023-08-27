using System;
using UnityEngine;

namespace Skill.Test
{
    public abstract class Effect : MonoBehaviour
    {
        [SerializeField] protected float timerToDisappear;
        [SerializeField] private float colorLosingSpeed = 1.5f;
        protected SpriteRenderer sr;

        protected virtual void Start()
        {
            sr = GetComponent<SpriteRenderer>();
        }

        protected virtual void Update()
        {
            timerToDisappear -= Time.deltaTime;
            if (timerToDisappear < 0)
                sr.color = new Color(1, 1, 1, sr.color.a - (Time.deltaTime * colorLosingSpeed));
            if(sr.color.a <= 0)
                Destroy(gameObject);
        }
    }
}