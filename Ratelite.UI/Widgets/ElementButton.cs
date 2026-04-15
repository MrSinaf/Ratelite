using Ratelite.Resources;

namespace Ratelite.UI.Widgets;

public class ElementButton : UIElement
{
	public readonly UIElement element;
	public bool isPressed { get; private set; }
	
	public event Action onClick = delegate { };
	public event Action<UIElement> onPressed = delegate { };
	public event Action<UIElement> onReleased = delegate { };
	
	public ElementButton(UIElement element, Action? onClick, string? prefab = "")
	{
		base.AddChild(this.element = element);
		this.onClick += onClick;
		
		element.isInteractif = false;
		R.game.window.mouseButtonPressed += OnMouseButtonPressed;
		R.game.window.mouseButtonReleased += OnMouseButtonReleased;
		UIPrefab.Apply(prefab, this);
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
				onClick.Invoke();
		}
	}
	
	public override void OnDestroy()
	{
		R.game.window.mouseButtonPressed -= OnMouseButtonPressed;
		R.game.window.mouseButtonReleased -= OnMouseButtonReleased;
	}
	
	[IsDefaultPrefab]
	public static void DefaultPrefrab(ElementButton e)
	{
		e.material = Vault.GetAsset<MaterialUI>(UIModule.DEFAULT_MATERIAL);
		e.mesh = Vault.GetAsset<Mesh>(UIModule.DEFAULT_MESH);
		e.size = new Vector2(200, 30);
		e.tint = new Color(0x26354A);
		e.cornerRadius = new Region(8);
		
		e.element.pivotAndAnchors = new Vector2(0.5F);
		
		e.cursorEnter += OnMouseEnter;
		e.cursorExit += OnMouseExit;
		
		void OnMouseExit(UIElement e)
			=> e.tint = new Color(0x26354A);
		
		void OnMouseEnter(UIElement e)
			=> e.tint = new Color(0x1C2739);
	}
}