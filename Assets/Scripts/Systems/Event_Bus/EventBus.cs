using System.Collections.Generic;

namespace Systems.Event_Bus
{
	public static class EventBus<T> where T : IEvent
	{
		private static readonly HashSet<IEventBinding<T>> _bindings = new();
		
		public static void Subscribe(EventBinding<T> binding) => _bindings.Add(binding);
		
		public static void Unsubscribe(EventBinding<T> binding) => _bindings.Remove(binding);

		public static void Publish(T @event)
		{
			foreach (IEventBinding<T> binding in _bindings)
			{
				binding.OnEvent.Invoke(@event);
				binding.OnEventNoArgs.Invoke();
			}
		}
	}
}