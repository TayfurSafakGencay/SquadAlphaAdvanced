Events.cs'nin içine istenildiği kadar class veya struct oluşturulabilir. Yapısı şu şekilde;

-------------------------------------------------

	public struct TestEvent : IEvent
	{
		public string Message { get; set; }
	}
	
	public struct TestEvent2 : IEvent{ }
	
--------------------------------------------------

Dinlemek isterken de;

---------------------------------------------------

	private EventBinding<TestEvent> _testEventBinding;
		
	private EventBinding<TestEvent2> _testEvent2Binding;

	private void OnEnable()
	{
		_testEventBinding = new EventBinding<TestEvent>(HandleTestEvent);
		_testEvent2Binding = new EventBinding<TestEvent2>(HandleTestEvent2);
			
		EventBus<TestEvent>.Subscribe(_testEventBinding);
		EventBus<TestEvent2>.Subscribe(_testEvent2Binding);
	}
		
	private void OnDisable()
	{
		EventBus<TestEvent>.Unsubscribe(_testEventBinding);
		EventBus<TestEvent2>.Unsubscribe(_testEvent2Binding);
	}