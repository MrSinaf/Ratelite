namespace Ratelite.UI.Widgets;

public class Layout : UIElement
{
	private bool isLocalDirty;
	
	public Orientation orientation
	{
		get;
		set
		{
			if (field == value)
				return;
			
			field = value;
			isLocalDirty = true;
		}
	} = Orientation.Vertical;
	public bool inverse
	{
		get;
		set
		{
			if (field == value)
				return;
			
			field = value;
			isLocalDirty = true;
		}
	}
	public float? alignment
	{
		get;
		set
		{
			field = value;
			isLocalDirty = true;
		}
	}
	public float spacing
	{
		get;
		set
		{
			field = value;
			isLocalDirty = true;
		}
	}
	
	
	public override void AddChild(UIElement element)
	{
		base.AddChild(element);
		element.elementChanged += OnElementChanged;
		isLocalDirty = true;
	}
	
	public override void RemoveChild(UIElement element)
	{
		base.RemoveChild(element);
		element.elementChanged -= OnElementChanged;
		isLocalDirty = true;
	}
	
	protected override void BeginUpdate()
	{
		if (isLocalDirty)
			ReArrangeChildren();
	}
	
	private void OnElementChanged(UIElement element) => isDirty = true;
	
	private void ReArrangeChildren()
	{
		size = Vector2.zero;
		var array = childrenArray;
		if (inverse && orientation == Orientation.Horizontal ||
			!inverse && orientation == Orientation.Vertical)
			Array.Reverse(array);
		
		for (var i = 0; i < array.Length; i++)
		{
			var child = array[i];
			if (child.isActif)
				SetChild(child, i);
		}
		
		isLocalDirty = false;
	}
	
	private void SetChild(UIElement element, int index)
	{
		// TODO > Il se base sur la size des UIElement, il faut plutôt gérer ça via leur realSize
		// Faut alors pouvoir générer leur taille avant d'afficher !
		var spacing = index == 0 ? 0 : this.spacing;
		anchorMax = anchorMin;	// S'assure que les anchors sont identiques.
		
		if (orientation == Orientation.Vertical)
		{
			element.position = new Vector2(0, size.y + spacing);
			if (alignment.HasValue)
			{
				element.pivot = new Vector2(alignment.Value, 0);
				element.anchorMin = new Vector2(alignment.Value, 0);
				element.anchorMax = new Vector2(alignment.Value, 0);
			}
			else
			{
				if (element.anchorMin != element.anchorMax)
					element.size = new Vector2(0, element.size.y);
				
				element.pivot = new Vector2(element.pivot.x, 0);
				element.anchorMin = new Vector2(element.anchorMin.x, 0);
				element.anchorMax = new Vector2(element.anchorMax.x, 0);
			}
			
			size = new Vector2(
				size.x < element.size.x ? element.size.x : size.x,
				size.y + element.size.y + spacing
			);
		}
		else
		{
			element.position = new Vector2(size.x + spacing, 0);
			if (alignment.HasValue)
			{
				element.pivot = new Vector2(0, alignment.Value);
				element.anchorMin = new Vector2(0, alignment.Value);
				element.anchorMax = new Vector2(0, alignment.Value);
			}
			else
			{
				if (element.anchorMin != element.anchorMax)
					element.size = new Vector2(element.size.x, 0);
				
				element.pivot = new Vector2(0, element.pivot.y);
				element.anchorMin = new Vector2(0, element.anchorMin.y);
				element.anchorMax = new Vector2(0, element.anchorMax.y);
			}
			
			size = new Vector2(
				size.x + element.size.x + spacing,
				size.y < element.size.y ? element.size.y : size.y
			);
		}
	}
}