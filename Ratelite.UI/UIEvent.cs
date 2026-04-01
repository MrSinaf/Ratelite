namespace Ratelite.UI;

public static class UIEvent
{
	/*
	 * Je ne suis pas très fan de ce que j'ai fait pour UIEvent...
	 * TODO > Rendre les événements pour l'UI plus intéressant, voir changer de système! (˘･_･˘)
	 */
	
	public enum Type
	{
		CursorEnter, CursorExit, CursorOver,
		ClickDown
	}
	
	private static readonly Dictionary<UIElement, Dictionary<Type, Action<UIElement>>> events = [];
	private static readonly List<(UIElement, Type, Action<UIElement>)> pendingRegistrations = [];
	private static readonly List<(UIElement, Type)> pendingUnRegistrations = [];
	
	private static readonly Dictionary<UIElement, HashSet<Type>> previousStatesElement = [];
	private static readonly HashSet<MouseButton> mouseButtonsPressed = [];
	
	static UIEvent()
	{
		Window.current.mouseButtonPressed += button => mouseButtonsPressed.Add(button);
		Window.current.mouseButtonReleased += button => mouseButtonsPressed.Remove(button);
	}
	
	public static void Register(UIElement element, Type eventType, Action<UIElement> action)
		=> pendingRegistrations.Add((element, eventType, action));
	
	public static void UnRegister(UIElement element, Type eventType)
		=> pendingUnRegistrations.Add((element, eventType));
	
	public static void UnRegisterAllEvents(UIElement element)
	{
		events.Remove(element);
		previousStatesElement.Remove(element);
	}
	
	internal static void Update()
	{
		ApplyPendingLists();
		
		foreach (var (element, events) in events)
		{
			if (!element.isActif)
				continue;
			
			var previousStates = previousStatesElement[element];
			var isMouseOver = element.ContainsPoint(Window.current.cursorPosition);
			var newStates = new HashSet<Type>();
			
			if (isMouseOver)
				newStates.Add(Type.CursorOver);
			
			foreach (var (eventType, action) in events)
			{
				switch (eventType)
				{
					case Type.CursorEnter:
						if (isMouseOver && !previousStates.Contains(Type.CursorOver))
						{
							action(element);
							newStates.Add(Type.CursorEnter);
						}
						
						break;
					case Type.CursorExit:
						if (!isMouseOver && previousStates.Contains(Type.CursorOver))
						{
							action(element);
							newStates.Add(Type.CursorExit);
						}
						
						break;
					case Type.CursorOver:
						if (isMouseOver)
							action(element);
						
						break;
					case Type.ClickDown:
						if (isMouseOver && mouseButtonsPressed.Contains(MouseButton.Left))
						{
							action(element);
							newStates.Add(Type.ClickDown);
						}
						
						break;
					default:
						throw new ArgumentOutOfRangeException();
				}
			}
			
			previousStatesElement[element] = newStates;
		}
	}
	
	private static void ApplyPendingLists()
	{
		foreach (var (element, eventType, action) in pendingRegistrations)
		{
			if (UIEvent.events.TryGetValue(element, out var events))
			{
				if (!events.TryAdd(eventType, action))
					events[eventType] += action;
			}
			else
			{
				UIEvent.events.Add(
					element,
					new Dictionary<Type, Action<UIElement>>
					{
						{
							eventType,
							action
						}
					}
				);
				previousStatesElement.Add(element, []);
			}
		}
		
		pendingRegistrations.Clear();
		
		foreach (var (element, eventType) in pendingUnRegistrations)
		{
			if (UIEvent.events.TryGetValue(element, out var events))
			{
				events.Remove(eventType);
				if (events.Count == 0)
				{
					UIEvent.events.Remove(element);
					previousStatesElement.Remove(element);
				}
			}
		}
		
		pendingUnRegistrations.Clear();
	}
}