using Ratelite.Resources;

namespace Ratelite.UI.Widgets;

public class Slider : UIElement
{
	public readonly UIElement cursor;
	public readonly Orientation orientation;
	
	public Action<float> onCursorChanged;
	
	private float _cursorPosition;
	public float cursorPosition
	{
		get => _cursorPosition;
		set
		{
			_cursorPosition = value;
			isCursorDirty = true;
		}
	}
	
	private float _value;
	public float value
	{
		get => _value;
		set
		{
			_value = value;
			isValueDirty = true;
		}
	}
	
	private Vector2 lastCursorPosition;
	private bool isCursorDirty;
	private bool isValueDirty;
	
	public Slider(Action<float> onCursorChanged, Orientation orientation, string? prefab = "")
	{
		this.onCursorChanged = onCursorChanged;
		this.orientation = orientation;
		base.AddChild(cursor = new UIElement());
		
		Window.current.mouseButtonPressed += OnMouseButtonPressed;
		Window.current.mouseButtonReleased += OnMouseButtonReleased;
		UIPrefab.Apply(prefab, this);
	}
	
	protected override void EndUpdate()
	{
		if (isCursorDirty)
			CursorPositionUpdated();
		
		if (isValueDirty)
			ValueUpdated();
	}
	
	private void ValueUpdated()
	{
		_cursorPosition = value * (realSize.x - cursor.realSize.x);
		ApplyUpdate();
	}
	
	private void CursorPositionUpdated()
	{
		float result;
		if (orientation == Orientation.Horizontal)
		{
			_cursorPosition = Math.Clamp(cursorPosition, 0, realSize.x - cursor.realSize.x);
			result = cursorPosition / (realSize.x - cursor.realSize.x);
		}
		else
		{
			_cursorPosition = Math.Clamp(cursorPosition, 0, realSize.y - cursor.realSize.y);
			result = cursorPosition / (realSize.y - cursor.realSize.y);
		}
		_value = result;
		ApplyUpdate();
	}
	
	private void ApplyUpdate()
	{
		onCursorChanged(value);
		cursor.position = orientation == Orientation.Horizontal
				? new Vector2(cursorPosition, 0)
				: new Vector2(0, cursorPosition);
		isCursorDirty = false;
		isValueDirty = false;
	}
	
	private void OnMouseButtonReleased(MouseButton button)
	{
		if (button == MouseButton.Left)
			Window.current.cursorMoved -= OnCursorMoved;
	}
	
	private void OnMouseButtonPressed(MouseButton button)
	{
		if (button == MouseButton.Left)
		{
			if (cursor.isCursorOver)
			{
				lastCursorPosition = Window.current.cursorPosition - cursor.position;
				Window.current.cursorMoved += OnCursorMoved;
			}
			else if (isCursorOver)
			{
				cursorPosition = Window.current.cursorPosition.x - realPosition.x - 
								 cursor.realSize.x * 0.5F;
				CursorPositionUpdated();
				lastCursorPosition = Window.current.cursorPosition - cursor.position;
				Window.current.cursorMoved += OnCursorMoved;
			}
		}
	}
	
	private void OnCursorMoved(Vector2 delta)
	{
		cursorPosition = orientation == Orientation.Horizontal
				? Window.current.cursorPosition.x - lastCursorPosition.x
				: Window.current.cursorPosition.y - lastCursorPosition.y;
	}
	
	[IsDefaultPrefab]
	public static void DefaultPrefab(Slider e)
	{
		e.mesh = Vault.GetAsset<Mesh>(UIModule.DEFAULT_MESH);
		e.material = Vault.GetAsset<MaterialUI>(UIModule.DEFAULT_MATERIAL);
		e.cornerRadius = new Region(5);
		e.size = e.orientation == Orientation.Horizontal
				? new Vector2(150, 15)
				: new Vector2(15, 150);
		
		e.cursor.mesh = Vault.GetAsset<Mesh>(UIModule.DEFAULT_MESH);
		e.cursor.material = Vault.GetAsset<MaterialUI>(UIModule.DEFAULT_MATERIAL);
		e.cursor.size = new Vector2(15);
		e.cursor.tint = Color.green;
		e.cursor.cornerRadius = new Region(5);
	}
}