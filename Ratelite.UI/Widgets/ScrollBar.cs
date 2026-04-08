using Ratelite.Resources;

namespace Ratelite.UI.Widgets;

public class ScrollBar : UIElement
{
	public readonly UIElement cursor;
	public readonly Orientation orientation;
	
	private float _cursorPosition;
	private Vector2 lastCursorPosition;
	
	public bool avalaibleIsGreatestContent { get; private set; }
	
	public float cursorPosition
	{
		get => _cursorPosition;
		set
		{
			_cursorPosition = value;
			isLocalDirty = true;
		}
	}
	
	public float contentLenght
	{
		get;
		set
		{
			if (field == value)
				return;
			
			field = value;
			avalaibleIsGreatestContent = availableLenght > contentLenght;
			ContentLenghtUpdated();
		}
	}
	
	public float availableLenght
	{
		get;
		set
		{
			if (field == value)
				return;
			
			field = value;
			avalaibleIsGreatestContent = availableLenght > contentLenght;
			ContentLenghtUpdated();
		}
	}
	
	public Action<float> onCursorChanged;
	
	private bool isLocalDirty;
	
	public ScrollBar(Action<float> onCursorChanged, Orientation orientation, string? prefab = "")
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
		// TODO > Chercher une manière de corriger le fait que cette action se fait tardivement
		// Pour le moment, nécessaire de le faire ici, pour que les enfants soient calculé (dans
		// le cas d'un Layout). Et il est nécessaire de le faire tardivement, car on peut vouloir
		// afficher la liste depuis le haut (voir ScrollView).
		if (isLocalDirty)
			CursorPositionUpdated();
	}
	
	private void CursorPositionUpdated()
	{
		if (availableLenght > contentLenght)
			return;
		
		float result;
		if (orientation == Orientation.Horizontal)
		{
			_cursorPosition = Math.Clamp(cursorPosition, 0, realSize.x - cursor.realSize.x);
			result = cursorPosition / realSize.x * contentLenght;
		}
		else
		{
			_cursorPosition = Math.Clamp(cursorPosition, 0, realSize.y - cursor.realSize.y);
			result = cursorPosition / realSize.y * contentLenght;
		}
		onCursorChanged(result);
		ContentLenghtUpdated();
	}
	
	private void ContentLenghtUpdated()
	{
		if (avalaibleIsGreatestContent)
		{
			cursor.scale = Vector2.one;
			cursor.position = Vector2.zero;
		}
		else if (orientation == Orientation.Horizontal)
		{
			cursor.scale = new Vector2(availableLenght / contentLenght, 1);
			cursor.position = new Vector2(cursorPosition, 0);
		}
		else
		{
			cursor.scale = new Vector2(1, availableLenght / contentLenght);
			cursor.position = new Vector2(0, cursorPosition);
		}
	}
	
	private void OnMouseButtonReleased(MouseButton button)
	{
		if (button == MouseButton.Left)
			Window.current.cursorMoved -= OnCursorMoved;
	}
	
	private void OnMouseButtonPressed(MouseButton button)
	{
		if (button == MouseButton.Left & cursor.ContainsPoint(Window.current.cursorPosition))
		{
			lastCursorPosition = Window.current.cursorPosition - cursor.position;
			Window.current.cursorMoved += OnCursorMoved;
		}
	}
	
	private void OnCursorMoved(Vector2 delta)
	{
		cursorPosition = orientation == Orientation.Horizontal
				? Window.current.cursorPosition.x - lastCursorPosition.x
				: Window.current.cursorPosition.y - lastCursorPosition.y;
	}
	
	[IsDefaultPrefab]
	public static void DefaultPrefab(ScrollBar e)
	{
		e.mesh = Vault.GetAsset<Mesh>(UIModule.DEFAULT_MESH);
		e.material = Vault.GetAsset<MaterialUI>(UIModule.DEFAULT_MATERIAL);
		e.cornerRadius = new Region(5);
		e.size = e.orientation == Orientation.Horizontal
				? new Vector2(150, 30)
				: new Vector2(30, 150);
		
		e.cursor.mesh = Vault.GetAsset<Mesh>(UIModule.DEFAULT_MESH);
		e.cursor.material = Vault.GetAsset<MaterialUI>(UIModule.DEFAULT_MATERIAL);
		e.cursor.anchorMin = Vector2.zero;
		e.cursor.anchorMax = Vector2.one;
		e.cursor.tint = Color.green;
		e.cursor.cornerRadius = new Region(5);
	}
}