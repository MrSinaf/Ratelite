using Ratelite.Resources;

namespace Ratelite.UI.Widgets;

public class Button : UIElement
{
	public readonly Label label;
	
	public string text { get => label.text; set => label.text = value; }
	
	public event Action onClick = delegate { };
	
	public Button(string text, Action? onClick, string prefab = "")
	{
		base.AddChild(label = new Label(text));
		this.onClick += onClick;
		
		R.game.window.mouseButtonPressed += OnMouseButton;
		UIPrefab.Apply(prefab, this);
	}
	
	private void OnMouseButton(MouseButton button)
	{
		if (button == MouseButton.Left && ContainsPoint(R.game.window.cursorPosition))
			onClick.Invoke();
	}
	
	public override void OnDestroy()
	{
		R.game.window.mouseButtonPressed -= OnMouseButton;
	}
	
	[IsDefaultPrefab]
	public static void DefaultPrefrab(Button e)
	{
		e.material = Vault.GetAsset<MaterialUI>(UIModule.DEFAULT_MATERIAL);
		e.mesh = Vault.GetAsset<Mesh>(UIModule.DEFAULT_MESH);
		e.size = new Vector2(200, 30);
		e.tint = new Color(0x26354A);
		e.cornerRadius = new Region(8);
		
		e.label.pivot = e.label.anchors = new Vector2(0.5F);
		
		UIEvent.Register(e, UIEvent.Type.CursorEnter, OnMouseEnter);
		UIEvent.Register(e, UIEvent.Type.CursorExit, OnMouseExit);
		
		void OnMouseExit(UIElement e)
			=> e.tint = new Color(0x26354A);
		
		void OnMouseEnter(UIElement e)
			=> e.tint = new Color(0x1C2739);
	}
}