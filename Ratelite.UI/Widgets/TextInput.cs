using Ratelite.Resources;

namespace Ratelite.UI.Widgets;

// TODO > Compléter cette class avec divers action et SURTOUT ajout un 'caret' (/≧▽≦)/
public class TextInput : UIElement
{
	public readonly Label placeholderLabel;
	public readonly Label valueLabel;
	
	public readonly UIElement caret;
	
	private CancellationTokenSource cancellationBlinks = new ();
	private bool ctrlHold;
	
	public event Action<string> onValueChanged = delegate { };
	
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
			onValueChanged(value);
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
				w.keyReleased += OnKeyReleased;
				
				cancellationBlinks = new CancellationTokenSource();
				BlinksCaret(cancellationBlinks.Token);
			}
			else
			{
				w.charTyped -= OnCharTyped;
				w.keyPressed -= OnKeyPressed;
				w.keyReleased -= OnKeyReleased;
				
				caret.visible = false;
				cancellationBlinks.Cancel();
			}
		}
	}
	
	public int caretPosition
	{
		get;
		private set
		{
			field = value;
			caret.visible = true;
			cancellationBlinks.Cancel();
			cancellationBlinks = new CancellationTokenSource();
			BlinksCaret(cancellationBlinks.Token);
			caret.position = new Vector2(valueLabel.font!.CalculTextSize(this.value[..field]).x, 0);
		}
	}
	
	private bool mousePressed;
	
	public TextInput(string? prefab = "")
	{
		var mask = new Mask
		{
			anchorMin = Vector2.zero, anchorMax = Vector2.one, isInteractif = false
		};
		mask.AddChild(placeholderLabel = new Label { name = "placeholder" });
		mask.AddChild(valueLabel = new Label { name = "value" });
		valueLabel.AddChild(caret = new UIElement { name = "caret", visible = false });
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
		var parentWidth = valueLabel.parent!.realSize.x;
		var textWidth = valueLabel.font!.CalculTextSize(value).x;
		
		if (textWidth <= parentWidth)
		{
			valueLabel.position = Vector2.zero;
			return;
		}
		
		var visibleWidth = parentWidth - caret.size.x;
		var currentOffset = valueLabel.position.x;
		var caretX = caret.position.x + currentOffset;
		
		if (caretX > visibleWidth)
			currentOffset -= caretX - visibleWidth;
		else if (caretX < 0)
			currentOffset -= caretX;
		var minOffset = visibleWidth - textWidth;
		if (currentOffset < minOffset)
			currentOffset = minOffset;
		if (currentOffset > 0)
			currentOffset = 0;
		
		valueLabel.position = new Vector2(currentOffset, 0);
	}
	
	private async void BlinksCaret(CancellationToken token)
	{
		try
		{
			while (!token.IsCancellationRequested)
			{
				caret.visible = true;
				await Task.Delay(500, token);
				caret.visible = false;
				await Task.Delay(500, token);
			}
		}
		catch { }
	}
	
	private void OnCharTyped(char c)
	{
		if (ctrlHold)
			return;
		
		value = value.Insert(caretPosition, c.ToString());
		caretPosition++;
		UpdateLabelPosition();
	}
	
	private void OnKeyPressed(Key key, int _)
	{
		switch (key)
		{
			case Key.Backspace:
				if (value.Length > 0)
				{
					value = value.Remove(caretPosition - 1, 1);
					caretPosition--;
				}
				break;
			case Key.Delete:
				if (value.Length >= caretPosition + 1)
					value = value.Remove(caretPosition, 1);
				break;
			case Key.Left:
				if (caretPosition > 0)
					caretPosition--;
				break;
			case Key.Right:
				if (value.Length > caretPosition)
					caretPosition++;
				break;
			case Key.LeftCtrl:
				ctrlHold = true;
				break;
			case Key.V when ctrlHold:
				value = value.Insert(
					caretPosition,
					Window.current.GetClipboardText() ?? string.Empty
				);
				break;
		}
		UpdateLabelPosition();
	}
	
	private void OnKeyReleased(Key key, int _)
	{
		ctrlHold = key switch { Key.LeftCtrl => false, _ => ctrlHold };
	}
	
	private void OnMouseButtonPressed(MouseButton button)
	{
		if (button != MouseButton.Left || !(focus = isCursorOver))
			return;
		
		mousePressed = true;
		caretPosition = valueLabel.font!.GetIndexCharInPosition(
			value,
			Window.current.cursorPosition.x - valueLabel.realPosition.x
		);
		UpdateLabelPosition();
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
		e.tint = new Color(0x1D2939);
		e.padding = new Region(2);
		e.size = new Vector2(200, 30);
		e.mesh = Vault.GetAsset<Mesh>(UIModule.DEFAULT_MESH);
		e.material = Vault.GetAsset<MaterialUI>(UIModule.DEFAULT_MATERIAL);
		e.cornerRadius = new Region(5);
		
		e.caret.mesh = Vault.GetAsset<Mesh>(UIModule.DEFAULT_MESH);
		e.caret.material = Vault.GetAsset<MaterialUI>(UIModule.DEFAULT_MATERIAL);
		e.caret.size = new Vector2Int(2, 0);
		e.caret.anchorMin = Vector2.zero;
		e.caret.anchorMax = Vector2.top;
		e.caret.overflowHidden = false;
		
		e.placeholderLabel.opacity = 0.5F;
		
		e.valueLabel.pivotAndAnchors = e.placeholderLabel.pivotAndAnchors = new Vector2(0, 0.5F);
	}
}