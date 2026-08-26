namespace Ratelite.Inputs;

public static class Input
{
	private static readonly HashSet<Key> keysHolded = [];
	private static readonly HashSet<MouseButton> mouseButtonsHolded = [];
	
	public static event Action<Key> keyPressed = delegate {};
	public static event Action<Key> keyHolded = delegate {};
	public static event Action<Key> keyReleased = delegate {};
	public static event Action<MouseButton> mouseButtonPressed = delegate {};
	public static event Action<MouseButton> mouseButtonHolded = delegate {};
	public static event Action<MouseButton> mouseButtonReleased = delegate {};
	
	internal static void Init()
	{
		R.game.window.keyPressed += OnKeyPressed;
		R.game.window.keyReleased += OnKeyReleased;
		R.game.window.mouseButtonPressed += OnMouseButtonPressed;
		R.game.window.mouseButtonReleased += OnButtonReleased;
	}
	
	internal static void Update()
	{
		foreach (var key in keysHolded)
			keyHolded.Invoke(key);
		
		foreach (var button in mouseButtonsHolded)
			mouseButtonHolded.Invoke(button);
	}
	
	private static void OnMouseButtonPressed(MouseButton button)
	{
		mouseButtonsHolded.Add(button);
		mouseButtonPressed.Invoke(button);
	}
	
	private static void OnButtonReleased(MouseButton button)
	{
		mouseButtonsHolded.Remove(button);
		mouseButtonReleased.Invoke(button);
	}
	
	private static void OnKeyPressed(Key key, int _)
	{
		keysHolded.Add(key);
		keyPressed.Invoke(key);
	}
	
	private static void OnKeyReleased(Key key, int _)
	{
		keysHolded.Remove(key);
		keyReleased.Invoke(key);
	}
	
	public static bool IsKeyHold(Key key) => keysHolded.Contains(key);
}