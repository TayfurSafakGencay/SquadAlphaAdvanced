namespace Actor.CharacterMovement.States
{
	public class MovingState : CharacterState
	{
		public MovingState(PlayerController controller) : base(controller)
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
			Controller.MoveCharacter();
		}
	}
}