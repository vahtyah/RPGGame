namespace Player
{
    public class PlayerStateMachine
    {
        private PlayerState state;

        public PlayerState State
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