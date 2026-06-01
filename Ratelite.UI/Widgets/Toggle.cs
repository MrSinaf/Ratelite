using Ratelite.Resources;

namespace Ratelite.UI.Widgets;

public class Toggle : ElementToggle
{
	public readonly Label label;
	
	public string textTrue
	{
		get;
		set
		{
			field = value;
			UpdateLabel();
		}
	}
	public string textFalse
	{
		get;
		set
		{
			field = value;
			UpdateLabel();
		}
	}
	
	public Toggle(string textTrue, string textFalse, Action<bool>? onToggle, string? prefab = "")
			: base(new Label(textFalse), onToggle)
	{
		label = (Label)element;
		this.onToggle += OnClick;
		this.textTrue = textTrue;
		this.textFalse = textFalse;
		UIPrefab.Apply(prefab, this);
	}
	
	private void OnClick(bool value)=> UpdateLabel();
	private void UpdateLabel() => label.text = value ? textTrue : textFalse;
	
	[IsDefaultPrefab]
	public static void DefaultPrefrab(Toggle e)
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