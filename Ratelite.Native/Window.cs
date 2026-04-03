using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using Ratelite.Bindings;
using Ratelite.Rendering;
using static Ratelite.Bindings.GlfwNative;

namespace Ratelite;

public sealed unsafe class Window
{
	public enum DisplayMode { Window, Fullscreen, NoBorder }
	
	public const uint BACKGROUND_HEX = 0x122333;
	
	public static Window current { get; private set; } = null!;
	
	private static bool initialized;
	
	internal readonly GLFWwindow* handle;
	
	public event Action start = delegate { };
	public event Action update = delegate { };
	public event Action render = delegate { };
	
	public event Action cursorEnter = delegate { };
	public event Action cursorExit = delegate { };
	public event Action<Vector2> cursorMoved = delegate { };
	public event Action<Vector2Int> scrolled = delegate { };
	
	public event Action<MouseButton> mouseButtonPressed = delegate { };
	public event Action<MouseButton> mouseButtonReleased = delegate { };
	
	public event Action<Key, int> keyPressed = delegate { };
	public event Action<Key, int> keyReleased = delegate { };
	public event Action<char> charTyped = delegate { };
	
	public event Action<Vector2> scaled = delegate { };
	public event Action<Vector2Int> resized = delegate { };
	public event Action<Vector2Int> moved = delegate { };
	
	public Vector2Int frameBufferSize { get; private set; }
	public Vector2 scale { get; private set; }
	public Vector2 cursorPosition { get; private set; } = new (-1);
	
	public Vector2Int position
	{
		get;
		set
		{
			field = value;
			glfwSetWindowPos(handle, field.x, field.y);
		}
	}
	public Vector2Int size
	{
		get;
		set
		{
			field = value;
			glfwSetWindowSize(handle, field.x, field.y);
		}
	}
	public DisplayMode displayMode
	{
		get;
		set
		{
			if (field == value)
				return;
			
			if (field == DisplayMode.Window && value != DisplayMode.Window)
			{
				int wx, wy, ww, wh;
				glfwGetWindowPos(handle, &wx, &wy);
				glfwGetWindowSize(handle, &ww, &wh);
			}
			
			ApplyDisplayMode(value);
			field = value;
		}
	}
	public bool resizable
	{
		get => glfwGetWindowAttrib(handle, Hints.RESIZABLE) == 1;
		set => glfwSetWindowAttrib(handle, Hints.RESIZABLE, value ? 1 : 0);
	}
	public bool decorated
	{
		get => glfwGetWindowAttrib(handle, Hints.DECORATED) == 1;
		set => glfwSetWindowAttrib(handle, Hints.DECORATED, value ? 1 : 0);
	}
	public int samples
	{
		get => glfwGetWindowAttrib(handle, Hints.SAMPLES);
		set => glfwSetWindowAttrib(handle, Hints.SAMPLES, value);
	}
	public bool visible
	{
		get => glfwGetWindowAttrib(handle, Hints.VISIBLE) == 1;
		set => glfwSetWindowAttrib(handle, Hints.VISIBLE, value ? 1 : 0);
	}
	
	private Window(GLFWwindow* handle) => this.handle = handle;
	
	public static Window Create(WindowOptions options, Window? shared = null)
	{
		EnsureInit();
		glfwDefaultWindowHints();
		glfwWindowHint(Hints.TRANSPARENT_FRAMEBUFFER, options.transparent ? 1 : 0);
		
		Console.OutputEncoding = Encoding.UTF8;
		var monitor = options.fullscreen ? glfwGetPrimaryMonitor() : null;
		GLFWwindow* handle;
		fixed (byte* titleUTF8 = Encoding.UTF8.GetBytes(options.title + "\0"))
			handle = glfwCreateWindow(
				options.width,
				options.height,
				(sbyte*)titleUTF8,
				monitor,
				shared != null ? shared.handle : null
			);
		
		return handle == null
				? throw new InvalidOperationException(
					"GLFW window creation failed: " + GetLastError()
				)
				: new Window(handle)
				{
					size = new Vector2Int(options.width, options.height),
					decorated = options.decorated,
					resizable = options.resizable,
					samples = options.samples,
					visible = options.visible
				};
	}
	
	public void Run()
	{
		var stopwatch = Stopwatch.StartNew();
		var lastTime = 0.0;
		
		MakeContextCurrent();
		glfwSwapInterval(1);
		
		GLNative.Load();
		GL.Enable(EnableCap.Blend);
		GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
		
		start();
		
		int width, height;
		glfwGetFramebufferSize(handle, &width, &height);
		frameBufferSize = new Vector2Int(width, height);
		
		glfwSetKeyCallback(handle, &OnKey);
		glfwSetCharCallback(handle, &OnChar);
		
		glfwSetMouseButtonCallback(handle, &OnMouseButton);
		glfwSetCursorPosCallback(handle, &OnCursorPos);
		glfwSetCursorEnterCallback(handle, &OnCursorEnter);
		glfwSetScrollCallback(handle, &OnCursorScroll);
		
		float xscale, yscale;
		glfwGetWindowContentScale(handle, &xscale, &yscale);
		scale = new Vector2(xscale, yscale);
		
		glfwSetWindowContentScaleCallback(handle, &OnWindowContentScale);
		glfwSetFramebufferSizeCallback(handle, &OnFramebufferResize);
		glfwSetWindowSizeCallback(handle, &OnWindowResize);
		glfwSetWindowPosCallback(handle, &OnWindowMove);
		while (glfwWindowShouldClose(handle) == 0)
		{
			PollEvents();
			
			var now = stopwatch.Elapsed.TotalSeconds;
			Time.Update(now - lastTime);
			lastTime = now;
			
			update();
			
			Render();
		}
	}
	
	public void SetTitle(string title)
	{
		fixed (byte* titleUTF8 = Encoding.UTF8.GetBytes(title + "\0"))
			glfwSetWindowTitle(handle, (sbyte*)titleUTF8);
	}
	
	public void SetIcon(RawImage image)
	{
		fixed (byte* data = image.pixels)
		{
			var glfWimage = new GLFWimage
			{
				width = image.size.x,
				height = image.size.y,
				pixels = data
			};
			glfwSetWindowIcon(handle, 1, &glfWimage);
		}
	}
	
	public void SetPosition(int x, int y)
		=> glfwSetWindowPos(handle, x, y);
	
	public void Center()
	{
		var monitor = glfwGetPrimaryMonitor();
		if (monitor == null) return;
		
		var mode = glfwGetVideoMode(monitor);
		if (mode == null) return;
		
		int wWidth, wHeight, aX, aY, aWidth, aHeight;
		glfwGetWindowSize(handle, &wWidth, &wHeight);
		glfwGetMonitorWorkarea(monitor, &aX, &aY, &aWidth, &aHeight);
		glfwSetWindowPos(
			handle,
			aX + (aWidth - wWidth) / 2,
			aY + (aHeight - wHeight) / 2
		);
	}
	
	public void Focus()
		=> glfwFocusWindow(handle);
	
	public void MakeContextCurrent()
	{
		glfwMakeContextCurrent(handle);
		current = this;
	}
	
	public void SwapBuffers()
		=> glfwSwapBuffers(handle);
	
	public void Close()
		=> glfwSetWindowShouldClose(handle, 1);
	
	public void Destroy()
		=> glfwDestroyWindow(handle);
	
	public static void PollEvents()
		=> glfwPollEvents();
	
	public void Hide()
		=> glfwHideWindow(handle);
	
	public void Show()
		=> glfwShowWindow(handle);
	
	private void Render()
	{
		GL.Clear(ClearBufferMask.ColorBufferBit);
		render();
		SwapBuffers();
	}
	
	[UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
	private static void OnKey(GLFWwindow* window, int key, int scancode, int action, int mods)
	{
		switch (action)
		{
			case 0:
				current.keyReleased((Key)key, scancode);
				break;
			case 1:
				current.keyPressed((Key)key, scancode);
				break;
		}
	}
	
	[UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
	private static void OnChar(GLFWwindow* window, uint c)
		=> current.charTyped((char)c);
	
	[UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
	public static void OnWindowContentScale(GLFWwindow* window, float xscale, float yscale)
		=> current.scaled(current.scale = new Vector2(xscale, yscale));
	
	[UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
	private static void OnFramebufferResize(GLFWwindow* window, int width, int height)
	{
		current.frameBufferSize = new Vector2Int(width, height);
		GL.Viewport(0, 0, (uint)current.frameBufferSize.x, (uint)current.frameBufferSize.y);
	}
	
	[UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
	private static void OnMouseButton(GLFWwindow* window, int button, int action, int mods)
	{
		if (action == 0)
			current.mouseButtonReleased((MouseButton)button);
		else
			current.mouseButtonPressed((MouseButton)button);
	}
	
	[UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
	private static void OnCursorPos(GLFWwindow* window, double x, double y)
	{
		var oldCursorPosition = current.cursorPosition;
		current.cursorPosition = new Vector2((float)x + 1, current.size.y - (float)y);
		current.cursorMoved(current.cursorPosition - oldCursorPosition);
	}
	
	[UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
	private static void OnCursorEnter(GLFWwindow* window, int entered)
	{
		if (entered == 1)
			current.cursorEnter();
		else
			current.cursorExit();
	}
	
	[UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
	private static void OnCursorScroll(GLFWwindow* window, double xoffset, double yoffset)
		=> current.scrolled(new Vector2Int((int)xoffset, (int)yoffset));
	
	[UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
	private static void OnWindowResize(GLFWwindow* window, int width, int height)
		=> current.resized(current.size = new Vector2Int(width, height));
	
	[UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
	private static void OnWindowMove(GLFWwindow* window, int width, int height)
		=> current.moved(current.position = new Vector2Int(width, height));
	
	private static void EnsureInit()
	{
		if (initialized)
			return;
		
		// ╰(*°▽°*)╯ charge les dll natives dépendantes
		ResolveNativeLibrary("glfw3");
		ResolveNativeLibrary("freetype");
		
		if (glfwInit() == 0)
			throw new InvalidOperationException("GLFW init failed: " + GetLastError());
		
		initialized = true;
	}
	
	private static string GetLastError()
	{
		sbyte* description;
		glfwGetError(&description);
		if (description == null)
			return "Unknown error";
		
		return Marshal.PtrToStringUTF8((nint)description) ?? "Unknown error";
	}
	
	private void ApplyDisplayMode(DisplayMode mode)
	{
		var monitor = glfwGetPrimaryMonitor();
		if (monitor == null)
			return;
		
		var vm = glfwGetVideoMode(monitor);
		if (vm == null)
			return;
		
		switch (mode)
		{
			case DisplayMode.Window:
			{
				glfwSetWindowMonitor(handle, null, 0, 0, 0, 0, 0);
				glfwSetWindowAttrib(handle, Hints.DECORATED, 1);
				
				position = new Vector2Int(100, 100);
				size = new Vector2Int(1280, 720);
				break;
			}
			
			case DisplayMode.Fullscreen:
			{
				glfwSetWindowAttrib(handle, Hints.DECORATED, 1);
				glfwSetWindowMonitor(handle, monitor, 0, 0, vm->width, vm->height, vm->refreshRate);
				position = new Vector2Int(0, 0);
				size = new Vector2Int(vm->width, vm->height);
				break;
			}
			
			case DisplayMode.NoBorder:
			{
				glfwSetWindowMonitor(handle, null, 0, 0, 0, 0, 0);
				glfwSetWindowAttrib(handle, Hints.DECORATED, 0);
				position = new Vector2Int(0, 0);
				size = new Vector2Int(vm->width, vm->height);
				break;
			}
			
			default:
				throw new ArgumentOutOfRangeException(nameof(mode), mode, null);
		}
	}
	
	private static void ResolveNativeLibrary(string libraryName)
	{
		var nativeLibExtension = RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
				? "{0}.dll"
				: RuntimeInformation.IsOSPlatform(OSPlatform.Linux) ? "lib{0}.so" : "lib{0}.dylib";
		var nativeLibPath = Path.Combine(
			AppContext.BaseDirectory,
			"runtimes",
			RuntimeInformation.RuntimeIdentifier,
			"native",
			string.Format(nativeLibExtension, libraryName)
		);
		
		if (File.Exists(nativeLibPath))
			NativeLibrary.Load(nativeLibPath);
		else
		{
			nativeLibPath = Path.Combine(
				AppContext.BaseDirectory,
				"packages",
				string.Format(nativeLibExtension, libraryName)
			);
			if (File.Exists(nativeLibPath))
				NativeLibrary.Load(nativeLibPath);
		}
	}
}

public readonly record struct WindowOptions(
	string title,
	int width,
	int height
)
{
	public bool decorated { get; init; } = true;
	public bool resizable { get; init; } = true;
	public bool fullscreen { get; init; } = false;
	public int samples { get; init; } = 1;
	public bool transparent { get; init; } = false;
	public bool visible { get; init; } = false;
}