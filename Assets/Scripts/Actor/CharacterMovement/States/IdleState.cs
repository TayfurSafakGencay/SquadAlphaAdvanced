namespace Actor.CharacterMovement.States
{
	public class IdleState : CharacterState
	{
		public IdleState(PlayerController controller) : base(controller)
		{
		}

		public override void Enter()
		{
		}
		
		public override void Exit()
		{
		}
				
		public override void Update()
		{
		// 	if (Controller.IsMovingInput())
		// 	{
		// 		// Controller.StateMachine.ChangeState(new MovingState(Controller));
		// 	}
		}
	}
}