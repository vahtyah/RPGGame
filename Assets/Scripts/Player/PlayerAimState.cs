namespace Player
{
    public class PlayerAimState : PlayerState
    {
        public PlayerAimState(PlayerStateMachine stateMachine, Player player, string animBoolName) : base(stateMachine, player, animBoolName)
        {
        }

        public override void Update()
        {
            base.Update();
            player.SetZeroVelocity();
        }

        public override void Enter()
        {
            base.Enter();
            player.StartCoroutine("BusyFor", .2f);

        }
    }
}