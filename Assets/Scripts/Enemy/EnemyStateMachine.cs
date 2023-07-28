using UnityEngine;

namespace Enemy
{
    public class EnemyStateMachine
    {
        private EnemyState state;

        public EnemyState State
        {
            get => state;
            set
            {
                state?.Exit();
                state = value;
                state?.Enter();
            }
        }
    }
}