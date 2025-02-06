namespace Event_Bus
{
	public struct TestEvent : IEvent
	{
		public string Message { get; set; }
	}
	
	public struct TestEvent2 : IEvent
	{
	}
}