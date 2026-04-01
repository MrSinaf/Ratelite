using Ratelite.Bindings;

namespace Ratelite.UI.Widgets;

public class Mask : UIElement
{
	private readonly Stack<Mask> masks = [];
	public bool masked = true;
	
	protected override void BeginRender()
	{
		masks.Push(this);
		ActiveMask();
	}
	
	protected override void EndRender()
	{
		masks.Pop();
		GL.Disable(EnableCap.ScissorTest);
		
		if (masks.TryPeek(out var mask))
			mask.ActiveMask();
	}
	
	private void ActiveMask()
	{
		if (masked)
		{
			GL.Scissor(
				(int)realPosition.x,
				(int)realPosition.y,
				(uint)realSize.x,
				(uint)realSize.y
			);
			GL.Enable(EnableCap.ScissorTest);
		}
	}
}