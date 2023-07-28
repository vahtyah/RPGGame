namespace Player
{
    public class PlayerStateMachine
    {
        private PlayerState currentState;

        public PlayerState CurrentState
        {
            get => currentState;
            set
            {
                currentState?.Exit();
                currentState = value;
                currentState?.Enter();
            }
        }
    }
}