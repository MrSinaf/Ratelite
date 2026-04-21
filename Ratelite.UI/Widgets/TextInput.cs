using Ratelite.Resources;

namespace Ratelite.UI.Widgets;

// TODO > Compléter cette class avec divers action et SURTOUT ajout un 'caret' (/≧▽≦)/
public class TextInput : UIElement
{
	public readonly Label placeholderLabel;
	public readonly Label valueLabel;
	
	public string placeholder
	{
		get => placeholderLabel.text;
		set => placeholderLabel.text = value;
	}
	
	public string value
	{
		get => valueLabel.text;
		set
		{
			if (value == string.Empty)
			{
				valueLabel.visible = false;
				placeholderLabel.visible = true;
			}
			else
			{
				valueLabel.visible = true;
				placeholderLabel.visible = false;
			}
			
			valueLabel.text = value;
		}
	}
	
	public bool focus
	{
		get;
		set
		{
			if (field == value)
				return;
			
			var w = Window.current;
			field = value;
			if (field)
			{
				w.charTyped += OnCharTyped;
				w.keyPressed += OnKeyPressed;
			}
			else
			{
				w.charTyped -= OnCharTyped;
				w.keyPressed -= OnKeyPressed;
			}
		}
	}
	
	private bool mousePressed;
	
	public TextInput(string? prefab = "")
	{
		var mask = new Mask
		{
			anchorMin = Vector2.zero, anchorMax = Vector2.one, isInteractif = false
		};
		mask.AddChild(placeholderLabel = new Label());
		mask.AddChild(valueLabel = new Label());
		base.AddChild(mask);
		
		var w = Window.current;
		w.mouseButtonPressed += OnMouseButtonPressed;
		w.mouseButtonReleased += OnMouseButtonReleased;
		UIPrefab.Apply(prefab, this);
	}
	
	public override void OnDestroy()
	{
		focus = false;
		var w = Window.current;
		w.mouseButtonPressed -= OnMouseButtonPressed;
		w.mouseButtonReleased -= OnMouseButtonReleased;
	}
	
	private void UpdateLabelPosition()
	{
		var parentSize = valueLabel.parent.realSize.x;
		var textSize = valueLabel.font.CalculTextSize(value);
		valueLabel.position = textSize > parentSize
				? new Vector2(-textSize + parentSize, 0)
				: Vector2.zero;
	}
	
	private void OnCharTyped(char c)
	{
		value += c;
		UpdateLabelPosition();
	}
	
	private void OnKeyPressed(Key key, int _)
	{
		switch (key)
		{
			case Key.Backspace:
				if (value.Length > 0)
					value = value.Remove(value.Length - 1, 1);
				break;
		}
		UpdateLabelPosition();
	}
	
	private void OnMouseButtonPressed(MouseButton button)
	{
		if (button != MouseButton.Left || !(focus = isCursorOver))
			return;
		
		mousePressed = true;
	}
	
	
	private void OnMouseButtonReleased(MouseButton button)
	{
		if (button != MouseButton.Left)
			return;
		
		mousePressed = false;
	}
	
	[IsDefaultPrefab]
	public static void DefaultPrefrab(TextInput e)
	{
		e.tint = new Color(0x26354A);
		e.padding = new Region(2);
		e.size = new Vector2(200, 30);
		e.mesh = Vault.GetAsset<Mesh>(UIModule.DEFAULT_MESH);
		e.material = Vault.GetAsset<MaterialUI>(UIModule.DEFAULT_MATERIAL);
		
		e.placeholderLabel.opacity = 0.5F;
		
		e.valueLabel.pivotAndAnchors = e.placeholderLabel.pivotAndAnchors = new Vector2(0, 0.5F);
	}
}