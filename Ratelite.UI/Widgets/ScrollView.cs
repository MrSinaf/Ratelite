namespace Ratelite.UI.Widgets;

public class ScrollView : UIElement
{
	public readonly ScrollBar hScroll;
	public readonly ScrollBar vScroll;
	private readonly Mask mask;
	
	private readonly UIElement content;
	
	public float scrollSpeed = 10;
	private bool isLocalDirty;
	
	public ScrollView(
		UIElement content,
		bool withHorizontal = true,
		bool withVertical = true,
		bool startOnTop = true,
		string? prefab = ""
	)
	{
		this.content = content;
		
		base.AddChild(mask = new Mask());
		base.AddChild(
			hScroll = new ScrollBar(OnHorizontalScroll, Orientation.Horizontal)
			{
				active = withHorizontal,
				cursorPosition = startOnTop ? float.MaxValue : 0
			}
		);
		base.AddChild(
			vScroll = new ScrollBar(OnVerticalScroll, Orientation.Vertical)
			{
				active = withVertical,
				cursorPosition = startOnTop ? float.MaxValue : 0
			}
		);
		mask.AddChild(content);
		R.game.window.scrolled += OnScroll;
		content.elementChanged += ContentChanged;
		elementChanged += OnChanged;
		UIPrefab.Apply(prefab, this);
	}
	
	protected override void EndUpdate()
	{
		if (isLocalDirty)
		{
			if (hScroll.active)
			{
				hScroll.availableLenght = mask.realSize.x;
				hScroll.contentLenght = content.realSize.x;
				hScroll.CursorPositionUpdated(false);
			}
			
			if (vScroll.active)
			{
				vScroll.availableLenght = mask.realSize.y;
				vScroll.contentLenght = content.realSize.y;
				vScroll.CursorPositionUpdated(false);
			}
			isLocalDirty = false;
			
			var result = new Vector2(-hScroll.cursorResult, -vScroll.cursorResult);
			if (content.position != result)
				content.position = result;
		}
	}
	
	private void OnScroll(Vector2Int delta)
	{
		if (isCursorOver)
		{
			vScroll.cursorPosition += delta.y * scrollSpeed;
			hScroll.cursorPosition += delta.x * scrollSpeed;
		}
		else if (hScroll.isCursorOver)
			hScroll.cursorPosition += delta.x * scrollSpeed;
		else if (vScroll.isCursorOver)
			vScroll.cursorPosition += delta.y * scrollSpeed;
	}
	
	private void OnChanged(UIElement _) => isLocalDirty = true;
	private void ContentChanged(UIElement _) => isLocalDirty = true;
	
	private void OnHorizontalScroll(float delta)
	{
		content.position = new Vector2(-delta, content.position.y);
	}
	
	private void OnVerticalScroll(float delta)
	{
		content.position = new Vector2(content.position.x, -delta);
	}
	
	public override void OnDestroy()
	{
		R.game.window.scrolled -= OnScroll;
	}
	
	[IsDefaultPrefab]
	public static void DefaultPrefab(ScrollView e)
	{
		const float size = 10;
		e.size = new Vector2(300, 300);
		
		var withV = e.vScroll.active;
		var withH = e.hScroll.active;
		
		e.mask.margin = new Region(0, withH ? size : 0, withV ? size : 0, 0);
		e.mask.anchorMin = Vector2.zero;
		e.mask.anchorMax = Vector2.one;
		
		e.vScroll.size = e.hScroll.size = new Vector2(size);
		
		e.hScroll.anchorMin = new Vector2(0, 0);
		e.hScroll.anchorMax = new Vector2(1, 0);
		e.hScroll.margin = new Region(0, 0,  withV ? size : 0, 0);
		
		e.vScroll.pivot = new Vector2(1, 0);
		e.vScroll.anchorMin = new Vector2(1, 0);
		e.vScroll.anchorMax = new Vector2(1, 1);
		e.vScroll.margin = new Region(0, withH ? size : 0, 0, 0);
	}
}