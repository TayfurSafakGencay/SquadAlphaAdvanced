using Actor.CharacterMovement.States.Base;

namespace Actor.CharacterMovement.States
{
	public class MovingState : CharacterState
	{
		public MovingState(PlayerController controller) : base(controller)
		{
						
		}

		public override void Enter()
		{
			SetupInputActions();
		}
		
		public override void Exit()
		{
		}
				
		public override void Update()
		{
            if(!Controller.IsMovingInput())
            {
				Controller.StateMachine.ChangeState(new IdleState(Controller));
				return;
            }
            
			Controller.MoveCharacter();
		}
		
		protected override void SetupInputActions()
		{
			InputManager.PlayerActions p = Controller.InputManager.Player;
			
			Controller.NewInputs(p.Move, p.Jump);
		}
	}
}