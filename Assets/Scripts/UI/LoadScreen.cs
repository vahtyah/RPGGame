using System;
using UnityEngine;

namespace UI
{
    public class LoadScreen : MonoBehaviour
    {
        public static LoadScreen Instance { get; private set; }

        private void Awake()
        {
            if (Instance) Destroy(gameObject);
            else Instance = this;
        }

        private Animator anim => GetComponent<Animator>();

        public void FadeIn() => anim.SetTrigger("fadeIn");
        public void FadeOut() => anim.SetTrigger("fadeOut");

    }
}