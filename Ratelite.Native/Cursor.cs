using Ratelite.Bindings;
using Ratelite.Rendering;
using static Ratelite.Bindings.GlfwNative;
using static Ratelite.Bindings.Hints;

namespace Ratelite;

public enum CursorMode
{
	Normal,
	Hidden,
	Disabled
}

public static unsafe class Cursor
{
	private static readonly List<(Vector2Int offset, nint handle)> cursors = [];
	private static int currentIndex = -1;
	
	public static CursorMode mode
	{
		get => throw new NotSupportedException("Ajoute un getter si tu bind glfwGetInputMode.");
		set
		{
			var glfwMode = value switch
			{
				CursorMode.Normal   => CURSOR_NORMAL,
				CursorMode.Hidden   => CURSOR_HIDDEN,
				CursorMode.Disabled => CURSOR_DISABLED,
				_                   => CURSOR_NORMAL
			};
			
			glfwSetInputMode(Window.current.handle, CURSOR, glfwMode);
		}
	}
	
	public static bool rawMouseMotion
	{
		set
		{
			if (glfwRawMouseMotionSupported() == FALSE)
				return;
			
			glfwSetInputMode(
				Window.current.handle,
				RAW_MOUSE_MOTION,
				value ? TRUE : FALSE
			);
		}
	}
	
	public static void AddTexture(RawImage texture, Vector2Int offset = new ())
	{
		fixed (byte* data = texture.pixels)
		{
			var image = new GLFWimage
			{
				width = texture.size.x,
				height = texture.size.y,
				pixels = data
			};
			
			var cursor = glfwCreateCursor(&image, offset.x, offset.y);
			if (cursor == null)
				throw new InvalidOperationException("Impossible de créer le curseur GLFW.");
			
			cursors.Add((offset, (nint)cursor));
		}
	}
	
	public static void SetTexture(int index)
	{
		if (currentIndex == index)
			return;
		
		var (_, cursor) = cursors[index];
		glfwSetCursor(Window.current.handle, (GLFWcursor*)cursor);
		currentIndex = index;
	}
	
	public static void ResetToDefault()
	{
		glfwSetCursor(Window.current.handle, null);
		currentIndex = -1;
	}
	
	public static void DestroyAll()
	{
		foreach (var (_, handle) in cursors)
		{
			var cursor = (GLFWcursor*)handle;
			if (cursor != null)
				glfwDestroyCursor(cursor);
		}
		
		cursors.Clear();
		currentIndex = -1;
	}
}