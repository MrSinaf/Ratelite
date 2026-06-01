namespace Ratelite.UI.Widgets;

public class ElementToggle : UIElement
{
	public readonly UIElement element;
	public bool isPressed { get; private set; }
	
	public bool value
	{
		get;
		set
		{
			field = value;
			onToggle.Invoke(field);
		}
	}
	
	public event Action<bool> onToggle = delegate { };
	public event Action<UIElement> onPressed = delegate { };
	public event Action<UIElement> onReleased = delegate { };
	
	public ElementToggle(UIElement element,  Action<bool>? onToggle)
	{
		base.AddChild(this.element = element);
		this.onToggle += onToggle;
		
		element.isInteractive = false;
		R.game.window.mouseButtonPressed += OnMouseButtonPressed;
		R.game.window.mouseButtonReleased += OnMouseButtonReleased;
	}
	
	private void OnMouseButtonPressed(MouseButton button)
	{
		if (button == MouseButton.Left && isCursorOver)
		{
			isPressed = true;
			onPressed(this);
		}
	}
	
	private void OnMouseButtonReleased(MouseButton button)
	{
		if (button == MouseButton.Left && isPressed)
		{
			isPressed = false;
			onReleased(this);
			
			if (isCursorOver)
				value = !value;
		}
	}
	
	public override void OnDestroy()
	{
		R.game.window.mouseButtonPressed -= OnMouseButtonPressed;
		R.game.window.mouseButtonReleased -= OnMouseButtonReleased;
	}
}