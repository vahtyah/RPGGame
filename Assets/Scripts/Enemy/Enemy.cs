using System;
using UnityEngine;

namespace Enemy
{
    public class Enemy : MonoBehaviour
    {
        public Rigidbody2D rb { get; private set; }
        public Animator animator { get; private set; }
        public EnemyStateMachine stateMachine { get; private set; }

        private void Awake()
        {
            stateMachine = new EnemyStateMachine();
            
        }

        private void Start()
        {
            animator = GetComponentInChildren<Animator>();
            rb = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            stateMachine.State.Update();
        }
    }
}