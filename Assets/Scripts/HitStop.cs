using System;
using System.Collections;
using UnityEngine;

namespace DefaultNamespace
{
    public class HitStop : MonoBehaviour
    {
        public static HitStop Instance { get; private set; }
        
        private bool waiting;

        private void Awake()
        {
            if(Instance) Destroy(gameObject);
            else Instance = this;
        }

        public void Stop(float duration)
        {
            if(waiting) return;
            Time.timeScale = 0f;
            StartCoroutine(Wait(duration));
        }

        private IEnumerator Wait(float duration)
        {
            waiting = true;
            yield return new WaitForSecondsRealtime(duration);
            Time.timeScale = 1f;
            waiting = false;
        }
    }
}