using Actor.CharacterMovement.States.Base;

namespace Actor.CharacterMovement.States
{
	public class IdleState : CharacterState
	{
		public IdleState(PlayerController controller) : base(controller)
		{
		}

		public override void Enter()
		{
			Controller.StopMovement();
			
			SetupInputActions();

			if (Controller.IsFiring)
			{
				Controller.Firing();
			}
		}
		
		public override void Exit()
		{
			

		}
				
		public override void Update()
		{
			if (Controller.IsMovingInput())
			{
				if (Controller.IsSprinting())
				{
					Controller.StateMachine.ChangeState(new SprintingState(Controller));
				}
				else
				{
					Controller.StateMachine.ChangeState(new MovingState(Controller));
				}
			}
		}
		
		protected override void SetupInputActions()
		{
			InputManager.PlayerActions p = Controller.InputManager.Player;
			
			Controller.NewInputs(p.Move, p.Jump, p.Attack, p.Sprint);
		}
	}
}