using Actor.CharacterMovement.States.Base;

namespace Actor.CharacterMovement
{
	public class CharacterStateMachine
	{
		private CharacterState _currentState;
		
		public void ChangeState(CharacterState newState)
		{
			_currentState?.Exit();
			_currentState = newState;
			_currentState.Enter();
		}
		
		public void UpdateState()
		{
			_currentState?.Update();
		}
	}
}