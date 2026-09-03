namespace Ratelite.Inputs;

public static class Input
{
	private static readonly HashSet<Key> keysHolded = [];
	private static readonly HashSet<MouseButton> mouseButtonsHolded = [];
	
	public static event Action<char> charTyped = delegate {};
	public static event Action<Key> keyPressed = delegate {};
	public static event Action<Key> keyHolded = delegate {};
	public static event Action<Key> keyReleased = delegate {};
	
	public static event Action<MouseButton> mouseButtonPressed = delegate {};
	public static event Action<MouseButton> mouseButtonHolded = delegate {};
	public static event Action<MouseButton> mouseButtonReleased = delegate {};
	
	public static event Action<Vector2> cursorMoved = delegate {};
	public static event Action<Vector2Int> scrolled = delegate { };
	
	public static Vector2 cursorPosition => R.game.window.cursorPosition;
	
	internal static void Init()
	{
		R.game.window.charTyped += OnCharTyped;
		R.game.window.keyPressed += OnKeyPressed;
		R.game.window.keyReleased += OnKeyReleased;
		R.game.window.mouseButtonPressed += OnMouseButtonPressed;
		R.game.window.mouseButtonReleased += OnButtonReleased;
		R.game.window.cursorMoved += OnCursorMoved;
		R.game.window.scrolled += OnScrolled;
	}
	
	internal static void Update()
	{
		foreach (var key in keysHolded)
			keyHolded.Invoke(key);
		
		foreach (var button in mouseButtonsHolded)
			mouseButtonHolded.Invoke(button);
	}
	
	private static void OnCharTyped(char c) => charTyped.Invoke(c);
	
	private static void OnCursorMoved(Vector2 delta) => cursorMoved.Invoke(delta);

	private static void OnScrolled(Vector2Int delta) => scrolled.Invoke(delta);
	
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