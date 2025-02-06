using System;

namespace Event_Bus
{
	public interface IEvent
	{
    
	}

	internal interface IEventBinding<T>
	{
		public Action<T> OnEvent { get; set; }
	
		public Action OnEventNoArgs { get; set; }
	}
}