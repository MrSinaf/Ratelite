using Ratelite.Resources;

namespace Ratelite.UI.Widgets;

public class Button : ElementButton
{
	public readonly Label label;
	public string text { get => label.text; set => label.text = value; }
	
	public Button(string text, Action? onClick, string? prefab = "") :
			base(new Label(text), onClick, null)
	{
		label = (Label)element;
		UIPrefab.Apply(prefab, this);
	}
	
	[IsDefaultPrefab]
	public static void DefaultPrefrab(Button e)
	{
		e.material = Vault.GetAsset<MaterialUI>(UIModule.DEFAULT_MATERIAL);
		e.mesh = Vault.GetAsset<Mesh>(UIModule.DEFAULT_MESH);
		e.size = new Vector2(200, 30);
		e.tint = new Color(0x26354A);
		e.cornerRadius = new Region(8);
		
		e.label.pivotAndAnchors = new Vector2(0.5F);
		
		e.cursorEnter += OnMouseEnter;
		e.cursorExit += OnMouseExit;
		
		void OnMouseExit(UIElement e)
			=> e.tint = new Color(0x26354A);
		
		void OnMouseEnter(UIElement e)
			=> e.tint = new Color(0x1C2739);
	}
}