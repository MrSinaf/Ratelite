namespace Ratelite.UI.Widgets;

public class ScrollView : UIElement
{
	public readonly ScrollBar horizontalScroll;
	public readonly ScrollBar verticalScroll;
	private readonly Mask mask;
	
	private readonly UIElement content;
	
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
			horizontalScroll = new ScrollBar(OnHorizontalScroll, Orientation.Horizontal)
			{
				active = withHorizontal,
				cursorPosition = startOnTop ? float.MaxValue : 0
			}
		);
		base.AddChild(
			verticalScroll = new ScrollBar(OnVerticalScroll, Orientation.Vertical)
			{
				active = withVertical,
				cursorPosition = startOnTop ? float.MaxValue : 0
			}
		);
		mask.AddChild(content);
		content.elementChanged += ContentChanged;
		elementChanged += OnChanged;
		UIPrefab.Apply(prefab, this);
	}
	
	private void OnChanged(UIElement _)
	{
		horizontalScroll.availableLenght = mask.realSize.x;
		horizontalScroll.contentLenght = content.realSize.x;
		verticalScroll.availableLenght = mask.realSize.y;
		verticalScroll.contentLenght = content.realSize.y; 
	}
	
	private void ContentChanged(UIElement _)
	{
		horizontalScroll.availableLenght = mask.realSize.x;
		horizontalScroll.contentLenght = content.realSize.x;
		verticalScroll.availableLenght = mask.realSize.y; 
		verticalScroll.contentLenght = content.realSize.y; 
	}
	
	private void OnHorizontalScroll(float delta)
	{
		content.position = new Vector2(-delta, content.position.y);
	}
	
	private void OnVerticalScroll(float delta)
	{
		content.position = new Vector2(content.position.x, -delta);
	}
	
	[IsDefaultPrefab]
	public static void DefaultPrefab(ScrollView e)
	{
		const float size = 10;
		e.size = new Vector2(300, 300);
		
		var withV = e.verticalScroll.active;
		var withH = e.horizontalScroll.active;
		
		e.mask.margin = new Region(0, withH ? size : 0, withV ? size : 0, 0);
		e.mask.anchorMin = Vector2.zero;
		e.mask.anchorMax = Vector2.one;
		
		e.verticalScroll.size = e.horizontalScroll.size = new Vector2(size);
		
		e.horizontalScroll.anchorMin = new Vector2(0, 0);
		e.horizontalScroll.anchorMax = new Vector2(1, 0);
		e.horizontalScroll.margin = new Region(0, 0,  withV ? size : 0, 0);
		
		e.verticalScroll.pivot = new Vector2(1, 0);
		e.verticalScroll.anchorMin = new Vector2(1, 0);
		e.verticalScroll.anchorMax = new Vector2(1, 1);
		e.verticalScroll.margin = new Region(0, withH ? size : 0, 0, 0);
	}
}