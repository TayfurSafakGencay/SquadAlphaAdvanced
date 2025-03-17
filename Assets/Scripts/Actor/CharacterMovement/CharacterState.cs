namespace Actor.CharacterMovement
{
    public abstract class CharacterState
    {
        protected PlayerController Controller { get; private set; }
        
        protected CharacterState(PlayerController controller)
        {
            Controller = controller;
        }
        
        public abstract void Enter();
        public abstract void Exit();
        public abstract void Update();
    }
}
