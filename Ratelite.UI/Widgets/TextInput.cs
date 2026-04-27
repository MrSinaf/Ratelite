using Ratelite.Resources;

namespace Ratelite.UI.Widgets;

public class TextInput : UIElement
{
	public readonly Label placeholderLabel;
	public readonly Label valueLabel;
	
	public readonly UIElement caret;
	public readonly UIElement selection;
	
	public HashSet<char>? charsAutorized;
	public uint maxLenght;
	
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
			
			selection.visible = false;
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
			caret.position = new Vector2(
				valueLabel.font!.CalculTextSize(this.value[..field]).x,
				0
			);
		}
	}
	
	public int selectionStart { get; private set; }
	
	public TextInput(string? prefab = "")
	{
		var mask = new Mask
		{
			anchorMin = Vector2.zero, anchorMax = Vector2.one
		};
		mask.AddChild(placeholderLabel = new Label { name = "placeholder" });
		mask.AddChild(valueLabel = new Label { name = "value" });
		valueLabel.AddChild(
			caret = new UIElement { name = "caret", visible = false, isInteractif = false }
		);
		valueLabel.AddChild(
			selection = new UIElement { name = "selection", visible = false, isInteractif = false }
		);
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
	
	private void UpdateSelectionPosition()
	{
		var font = valueLabel.font!;
		var min = int.Min(selectionStart, caretPosition);
		var max = int.Max(selectionStart, caretPosition);
		var minTextSize = font.CalculTextSize(value[..min]).x;
		var maxTextSize = font.CalculTextSize(value[..max]).x;
		selection.position = new Vector2(minTextSize, 0);
		selection.size = new Vector2(maxTextSize - minTextSize, 0);
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
		if (ctrlHold || charsAutorized?.Contains(c) == true ||
			(maxLenght > 0 && value.Length >= maxLenght))
			return;
		
		if (caretPosition == selectionStart)
		{
			value = value.Insert(caretPosition, c.ToString());
			selectionStart = ++caretPosition;
		}
		else
		{
			var min = int.Min(caretPosition, selectionStart);
			value = value.Remove(min, int.Max(caretPosition, selectionStart) - min);
			value = value.Insert(min, c.ToString());
			selectionStart = caretPosition = ++min;
		}
		UpdateLabelPosition();
	}
	
	private void OnKeyPressed(Key key, int _)
	{
		switch (key)
		{
			case Key.Backspace:
				if (value.Length > 0)
				{
					if (caretPosition == selectionStart)
					{
						value = value.Remove(caretPosition - 1, 1);
						selectionStart = --caretPosition;
					}
					else
					{
						var min = int.Min(caretPosition, selectionStart);
						value = value.Remove(min, int.Max(caretPosition, selectionStart) - min);
						selectionStart = caretPosition = min;
					}
				}
				break;
			case Key.Delete:
				if (value.Length >= caretPosition + 1)
				{
					if (selectionStart == caretPosition)
					{
						value = value.Remove(caretPosition, 1);
					}
					else
					{
						var min = int.Min(caretPosition, selectionStart);
						value = value.Remove(min, int.Max(caretPosition, selectionStart) - min);
						selectionStart = caretPosition = min;
					}
				}
				break;
			case Key.Left:
				if (caretPosition > 0)
				{
					selectionStart = --caretPosition;
					selection.visible = false;
				}
				break;
			case Key.Right:
				if (value.Length > caretPosition)
				{
					selectionStart = ++caretPosition;
					selection.visible = false;
				}
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
		
		Window.current.cursorMoved += OnCursorMoved;
		selection.visible = false;
		selectionStart = caretPosition = valueLabel.font!.GetIndexCharInPosition(
			value,
			Window.current.cursorPosition.x - valueLabel.realPosition.x
		);
		UpdateLabelPosition();
	}
	
	private void OnMouseButtonReleased(MouseButton button)
	{
		if (button != MouseButton.Left)
			return;
		
		Window.current.cursorMoved -= OnCursorMoved;
	}
	
	private void OnCursorMoved(Vector2 _)
	{
		caretPosition = valueLabel.font!.GetIndexCharInPosition(
			value,
			Window.current.cursorPosition.x - valueLabel.realPosition.x
		);
		selection.visible = true;
		
		UpdateSelectionPosition();
		UpdateLabelPosition();
		selection.visible = selectionStart != caretPosition;
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
		e.caret.size = new Vector2(2, 0);
		e.caret.anchorMin = Vector2.zero;
		e.caret.anchorMax = Vector2.top;
		e.caret.overflowHidden = false;
		
		e.selection.mesh = Vault.GetAsset<Mesh>(UIModule.DEFAULT_MESH);
		e.selection.material = Vault.GetAsset<MaterialUI>(UIModule.DEFAULT_MATERIAL);
		e.selection.opacity = 0.2F;
		e.selection.anchorMin = Vector2.zero;
		e.selection.anchorMax = Vector2.top;
		
		e.placeholderLabel.opacity = 0.5F;
		
		e.valueLabel.pivotAndAnchors = e.placeholderLabel.pivotAndAnchors = new Vector2(0, 0.5F);
	}
}