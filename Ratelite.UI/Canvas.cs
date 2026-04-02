namespace Ratelite.UI;

public class Canvas : IPlugin
{
	private readonly CanvasUniform uniform = new ();
	private readonly UIElement tree = new ();
	
	public RootElement root { get; private set; } = null!;
	public bool hasElementHovered => previousElementsHovered.Count > 0;
	
	private readonly Stack<UIElement> stackElementHover = [];
	private HashSet<UIElement> previousElementsHovered = [];
	
	public void Init()
	{
		var window = R.game.window;
		window.resized += OnWindowResized;
		
		root = new RootElement { name = "root", size = window.size, isInteractif = false };
		uniform.projection = Matrix3X3.CreateOrthographic(root.size.x, root.size.y, false);
		uniform.resolution = root.size;
	}
	
	public void Update()
	{
		root.InternalUpdate(tree, stackElementHover);
		
		var newElementsHovered = new HashSet<UIElement>();
		while (stackElementHover.TryPop(out var e))
		{
			if (!e.isInteractif)
				continue;
			
			var inPrevious = previousElementsHovered.Contains(e);
			previousElementsHovered.Remove(e);
			
			newElementsHovered.Add(e);
			if (!inPrevious)
				e.OnCursorEnter();
			
			if (e.captureCursorEvent)
				break;
		}
		
		foreach (var e in previousElementsHovered)
			e.OnCursorExit();
		
		stackElementHover.Clear();
		previousElementsHovered = newElementsHovered;
	}
	
	public void Render()
	{
		uniform.deltaTime = Time.delta;
		uniform.time = Time.total;
		uniform.UpdateBuffer();
		
		root.InternalRender();
	}
	
	public void Destroy()
	{
		root.Destroy();
		R.game.window.resized -= OnWindowResized;
	}
	
	private void OnWindowResized(Vector2Int size)
	{
		if (size.x <= 0 || size.y <= 0)
			return;
		
		root.size = uniform.resolution = size;
		uniform.projection = Matrix3X3.CreateOrthographic(root.size.x, root.size.y, false);
	}
}